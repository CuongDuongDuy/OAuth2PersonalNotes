using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using OAuth2PersonalNotes.Share;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models;

namespace OAuth2PersonalNotes.Web.AuthorizationMiddleware.Repositories
{
    public class AuthorizationRepository
    {
        private AuthorizationDbContext DbContext { get; set; }
        public AuthorizationRepository()
        {
            DbContext = AuthorizationDbContext.Create();
        }

        public IEnumerable<string> GetUserRoles(string userEmail)
        {
            var user = DbContext.Users.FirstOrDefault(x=>x.Email == userEmail);
            List<string> roles = null;
            if (user != null)
            {
                roles = user.Roles.Select(role => role.Name).ToList();
            }
            return roles;
        }

        public void SetToken(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> n)
        {
            var givenNameClaim = n.AuthenticationTicket
                .Identity.FindFirst(IdentityModel.JwtClaimTypes.GivenName);

            var familyNameClaim = n.AuthenticationTicket
                .Identity.FindFirst(IdentityModel.JwtClaimTypes.FamilyName);

            var emailClaim = n.AuthenticationTicket
                .Identity.FindFirst(IdentityModel.JwtClaimTypes.Email);

            var subClaim = n.AuthenticationTicket
                .Identity.FindFirst(IdentityModel.JwtClaimTypes.Subject);

            var nameClaim = new Claim(IdentityModel.JwtClaimTypes.Name,
                string.Format("{0}/{1}", Constants.NotesIssuerUri, subClaim.Value));

            var newClaimsIdentity = new ClaimsIdentity(
                n.AuthenticationTicket.Identity.AuthenticationType,
                IdentityModel.JwtClaimTypes.Name,
                IdentityModel.JwtClaimTypes.Role);

            newClaimsIdentity.AddClaim(nameClaim);

            if (givenNameClaim != null)
            {
                newClaimsIdentity.AddClaim(givenNameClaim);
            }

            if (familyNameClaim != null)
            {
                newClaimsIdentity.AddClaim(familyNameClaim);
            }

            if (emailClaim != null)
            {
                newClaimsIdentity.AddClaim(emailClaim);
                var roles = GetUserRoles(emailClaim.Value);
                foreach (var role in roles)
                {
                    newClaimsIdentity.AddClaim(new Claim(IdentityModel.JwtClaimTypes.Role, role));
                }
            }

            newClaimsIdentity.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

            n.AuthenticationTicket = new AuthenticationTicket(newClaimsIdentity,
                n.AuthenticationTicket.Properties);
        }
    }
}