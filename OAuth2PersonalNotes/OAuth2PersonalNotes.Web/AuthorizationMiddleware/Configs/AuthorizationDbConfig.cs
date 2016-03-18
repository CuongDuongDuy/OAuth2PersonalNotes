using System.Data.Entity;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models;

namespace OAuth2PersonalNotes.Web.AuthorizationMiddleware.Configs
{
    public class AuthorizationDbInitializer : DropCreateDatabaseIfModelChanges<AuthorizationDbContext>
    {
    }
}