using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using OAuth2PersonalNotes.IdentityServer;
using OAuth2PersonalNotes.IdentityServer.Config;
using OAuth2PersonalNotes.Share;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OAuth2PersonalNotes.IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.Map("/identity", idsrvApp =>
            {
                var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryUsers(Users.Get())
                    .UseInMemoryScopes(Scopes.Get());

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "Notes Security Token Service",
                    IssuerUri = Constants.NotesIssuerUri,
                    PublicOrigin = Constants.NotesStsOrigin,
                    SigningCertificate = LoadCertificate()
                };
                idsrvApp.UseIdentityServer(options);
            });
        }

        public X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\OAuth2PersonalNotes.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "abc@123");
        }
    }
}
