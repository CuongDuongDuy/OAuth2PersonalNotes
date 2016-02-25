using System.Data.Entity;

namespace OAuth2PersonalNotes.Api.Models
{
    public class NotesDbContext : DbContext
    {
        public DbSet<PersonalNote> PersonalNotes { get; set; }

        public NotesDbContext():base("NotesDatabase")
        {
            Database.SetInitializer(new NotesDbInitializer());
        }
    }

    public class NotesDbInitializer : DropCreateDatabaseIfModelChanges<NotesDbContext>
    {
        protected override void Seed(NotesDbContext context)
        {
            base.Seed(context);
        }
    }
}