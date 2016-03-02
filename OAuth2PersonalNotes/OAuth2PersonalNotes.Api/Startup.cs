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
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = Constants.NotesSts,
                    RequiredScopes = new[] { "notesmanagement" }
                });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
