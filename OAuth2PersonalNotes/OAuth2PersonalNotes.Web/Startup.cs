using Microsoft.Owin;
using OAuth2PersonalNotes.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
