using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Web.Helpers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using OAuth2PersonalNotes.Share;
using OAuth2PersonalNotes.Web;
using OAuth2PersonalNotes.Web.Helpers;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityModel.JwtClaimTypes.Name;

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "personalnoteshybrid",
                Authority = Share.Constants.NotesSts,
                RedirectUri = Share.Constants.NotesMvc,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token token",
                Scope = "openid profile notesuser",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var givenNameClaim = n.AuthenticationTicket
                           .Identity.FindFirst(IdentityModel.JwtClaimTypes.GivenName);

                        var familyNameClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.FamilyName);

                        var subClaim = n.AuthenticationTicket
                            .Identity.FindFirst(IdentityModel.JwtClaimTypes.Subject);

                        var nameClaim = new Claim(IdentityModel.JwtClaimTypes.Name,
                                    string.Format("{0}_{1}",Constants.NotesIssuerUri, subClaim.Value));

                        var newClaimsIdentity = new ClaimsIdentity(
                           n.AuthenticationTicket.Identity.AuthenticationType,
                           IdentityModel.JwtClaimTypes.Name,
                           IdentityModel.JwtClaimTypes.Role);

                        if (nameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(nameClaim);
                        }

                        if (givenNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(givenNameClaim);
                        }

                        if (familyNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(familyNameClaim);
                        }

                        // add the access token so we can access it later on 
                        newClaimsIdentity.AddClaim(
                            new Claim("access_token", n.ProtocolMessage.AccessToken));

                        // create a new authentication ticket, overwriting the old one.
                        n.AuthenticationTicket = new AuthenticationTicket(
                                                 newClaimsIdentity,
                                                 n.AuthenticationTicket.Properties);
                    }
                }
            });
        }
    }
}
