using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Web.Helpers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
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
                Scope = "openid profile",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async notification =>
                    {
                        TokenHelper.DecodeAndWrite(notification.ProtocolMessage.IdToken);
                        TokenHelper.DecodeAndWrite(notification.ProtocolMessage.AccessToken);
                    }
                }
            });
        }
    }
}
