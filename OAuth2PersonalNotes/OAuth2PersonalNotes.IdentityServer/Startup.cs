using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.Default;
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
                var corsPolicyService = new DefaultCorsPolicyService()
                {
                    AllowAll = true
                };
                var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                     .UseInMemoryScopes(Scopes.Get())
                    .UseInMemoryUsers(Users.Get());

                idServerServiceFactory.CorsPolicyService = new
                    Registration<IdentityServer3.Core.Services.ICorsPolicyService>(corsPolicyService);
                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "Notes Security Token Service",
                    SigningCertificate = LoadCertificate(),
                    IssuerUri = Constants.NotesIssuerUri,
                    PublicOrigin = Constants.NotesStsOrigin
                    
                };
                idsrvApp.UseIdentityServer(options);
            });
        }

        public X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\certificates\idsrv3test.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
