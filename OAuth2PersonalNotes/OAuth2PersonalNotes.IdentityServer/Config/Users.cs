using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace OAuth2PersonalNotes.IdentityServer.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "Admin",
                    Password = "secret",
                    Subject = "b05d3546-6ca8-4d32-b95c-77e94d705ddf",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Email, "Admin@PersonalNotes.com"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Duong"),
                        new Claim(Constants.ClaimTypes.GivenName, "Admin")
                    }

                },
                new InMemoryUser
                {
                    Username = "CuongDuong",
                    Password = "secret",
                    Subject = "bb61e881-3a49-42a7-8b62-c13dbe102018",
                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.Email, "CuongDuong@PersonalNotes.com"),
                         new Claim(Constants.ClaimTypes.FamilyName, "Duong"),
                        new Claim(Constants.ClaimTypes.GivenName, "Cuong")
                    }
                }
            };
        }
    }
}
