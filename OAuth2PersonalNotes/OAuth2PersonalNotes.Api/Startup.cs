using Microsoft.Owin;
using OAuth2PersonalNotes.Api;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
