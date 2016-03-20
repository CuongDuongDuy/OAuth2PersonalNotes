using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using OAuth2PersonalNotes.Api;
using OAuth2PersonalNotes.Share;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = Constants.NotesSts,
                RequiredScopes = new[] {"email"}
            });
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
