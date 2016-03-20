using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using OAuth2PersonalNotes.Share;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Repositories;
using Owin;

namespace OAuth2PersonalNotes.Web.AuthorizationMiddleware.Configs
{
    public class Register
    {
        public static void AuthorizationDatabase(IAppBuilder app)
        {
            //app.CreatePerOwinContext(AuthorizationDbContext.Create);
        }

        public static void AuthorizationServer(IAppBuilder app)
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
                Authority = Constants.NotesSts,
                RedirectUri = Constants.NotesMvc,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token token",
                Scope = "openid profile email",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var authorizationRespo = new AuthorizationRepository();
                        authorizationRespo.SetToken(n);
                    }
                }
            });
        }

        
    }
}