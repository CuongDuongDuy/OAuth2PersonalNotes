using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Configs;

namespace OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models
{
    public class AuthorizationDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthorizationDbContext() : base("AuthorizationDatabase")
        {

        }

        static AuthorizationDbContext()
        {
            Database.SetInitializer(new AuthorizationDbInitializer());
        }

        public static AuthorizationDbContext Create()
        {
            return new AuthorizationDbContext();
        }
    }
}