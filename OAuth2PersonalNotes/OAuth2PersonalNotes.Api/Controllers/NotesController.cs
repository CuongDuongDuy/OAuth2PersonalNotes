using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OAuth2PersonalNotes.Api.Models;

namespace OAuth2PersonalNotes.Api.Controllers
{
    [RoutePrefix("api/Notes")]
    public class NotesController : ApiController
    {
        private readonly NotesDbContext db = new NotesDbContext();

        [HttpGet]
        [Route("")]
        public IQueryable<PersonalNote> GetPersonalNotes()
        {
            return db.PersonalNotes;
        }

        [HttpGet]
        [Route("{id:int}",Name = "GetById")]
        [ResponseType(typeof(PersonalNote))]
        public async Task<IHttpActionResult> GetPersonalNote(int id)
        {
            var personalnote = await db.PersonalNotes.FindAsync(id);
            if (personalnote == null)
            {
                return NotFound();
            }

            return Ok(personalnote);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> PutPersonalNote(int id, PersonalNote personalnote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personalnote.Id)
            {
                return BadRequest();
            }

            db.Entry(personalnote).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalNoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PersonalNote))]
        public async Task<IHttpActionResult> PostPersonalNote(PersonalNote personalnote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonalNotes.Add(personalnote);
            await db.SaveChangesAsync();

            return CreatedAtRoute("GetById", new { id = personalnote.Id }, personalnote);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(PersonalNote))]
        public async Task<IHttpActionResult> DeletePersonalNote(int id)
        {
            var personalnote = await db.PersonalNotes.FindAsync(id);
            if (personalnote == null)
            {
                return NotFound();
            }

            db.PersonalNotes.Remove(personalnote);
            await db.SaveChangesAsync();

            return Ok(personalnote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalNoteExists(int id)
        {
            return db.PersonalNotes.Count(e => e.Id == id) > 0;
        }
    }
}