using Microsoft.Owin;
using OAuth2PersonalNotes.Api;
using Owin;
using IdentityServer3.AccessTokenValidation;
using Constants = OAuth2PersonalNotes.Share.Constants;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = Constants.NotesSts,
                    RequiredScopes = new[] { "notesmanagement", "notesuser" }
                });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
