using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace OAuth2PersonalNotes.IdentityServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        { 
            return new []
            {
                new Client 
                {                   
                     ClientId = "personalnotesauthcode",
                     ClientName = "Personal Note (Authorization Code)",
                     Flow = Flows.AuthorizationCode, 
                     AllowAccessToAllScopes = true,

                    // redirect = URI of the MVC application callback
                    RedirectUris = new List<string>
                    { 
                        Share.Constants.NotesMvcstsCallback
                    },           

                    // client secret
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(Share.Constants.NotesClientSecret.Sha256())
                    }                    
                } 
            }; 
        }
    }
}
