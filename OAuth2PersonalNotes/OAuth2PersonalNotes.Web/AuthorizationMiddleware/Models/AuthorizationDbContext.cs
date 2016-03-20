using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Configs;

namespace OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models
{
    public class AuthorizationDbContext : DbContext
    {
        public AuthorizationDbContext() : base("AuthorizationDatabase")
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }  

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