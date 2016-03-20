using Microsoft.Owin;
using OAuth2PersonalNotes.Web;
using Owin;
using Register = OAuth2PersonalNotes.Web.AuthorizationMiddleware.Configs.Register;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Register.AuthorizationServer(app);
            Register.AuthorizationDatabase(app);
        }
    }
}
