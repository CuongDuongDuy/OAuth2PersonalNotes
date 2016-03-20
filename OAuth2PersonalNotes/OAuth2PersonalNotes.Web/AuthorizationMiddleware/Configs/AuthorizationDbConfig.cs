using System.Collections.Generic;
using System.Data.Entity;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models;

namespace OAuth2PersonalNotes.Web.AuthorizationMiddleware.Configs
{
    public class AuthorizationDbInitializer : DropCreateDatabaseIfModelChanges<AuthorizationDbContext>
    {
        protected override void Seed(AuthorizationDbContext context)
        {
            var adminRole = new ApplicationRole
            {
                Name = Share.Constants.NotesAdminRole
            };
            var userRole = new ApplicationRole
            {
                Name = Share.Constants.NotesUserRole
            };

            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);

            var users = new[]
            {
                new ApplicationUser
                {
                    Email = "Admin@PersonalNotes.com",
                    Roles = new List<ApplicationRole>
                    {
                        adminRole,
                        userRole
                    }
                },
                new ApplicationUser
                {
                    Email = "CuongDuong@PersonalNotes.com",
                    Roles = new List<ApplicationRole>
                    {
                        userRole
                    }
                }
            };
            context.Users.AddRange(users);
            base.Seed(context);
        }
    }
}