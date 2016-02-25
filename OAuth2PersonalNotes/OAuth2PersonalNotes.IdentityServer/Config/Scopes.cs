using System.Collections.Generic;
using IdentityServer3.Core.Models;

namespace OAuth2PersonalNotes.IdentityServer.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new List<Scope>
                {                    
                    new Scope
                    { 
                        Name = "notesmanagement",
                        DisplayName = "Notes Management",
                        Description = "Allow the application to manage notes on your behalf.",
                        Type = ScopeType.Resource 
                    },
                    new Scope
                    { 
                        Name = "notesuser",
                        DisplayName = "Notes Normal User",
                        Description = "Allow the application to manage notes on your behalf (normal user).",
                        Type = ScopeType.Resource 
                    }
                };
        }
    }
}
