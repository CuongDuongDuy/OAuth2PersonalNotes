using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace OAuth2PersonalNotes.IdentityServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "personalnotesauthcode",
                    ClientName = "Personal Note (Authorization Code)",
                    Flow = Flows.AuthorizationCode,
                    AllowAccessToAllScopes = true,

                    RedirectUris = new List<string>
                    {
                        Share.Constants.NotesMvcstsCallback
                    },

                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(Share.Constants.NotesClientSecret.Sha256())
                    }
                },
                new Client
                {
                    ClientId = "personalnoteshybrid",
                    ClientName = "Personal Note (Hybrid)",
                    Flow = Flows.Hybrid,
                    AllowAccessToAllScopes = true,

                    RedirectUris = new List<string>
                    {
                        Share.Constants.NotesMvc
                    }
                }
            };
        }
    }
}
