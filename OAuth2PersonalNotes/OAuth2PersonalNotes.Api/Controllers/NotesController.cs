using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OAuth2PersonalNotes.Api.Models;
using OAuth2PersonalNotes.DTO;

namespace OAuth2PersonalNotes.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Notes")]
    public class NotesController : ApiController
    {
        private readonly NotesDbContext db = new NotesDbContext();

        public NotesController()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PersonalNote, Note>();
                cfg.CreateMap<Note, PersonalNote>();
            });
        }

        [HttpGet]
        [Route("")]
        public IQueryable<Note> GetPersonalNotes()
        {
            return db.PersonalNotes.ProjectTo<Note>(Mapper.Configuration);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetById")]
        [ResponseType(typeof(Note))]
        public async Task<IHttpActionResult> GetPersonalNote(int id)
        {
            var personalNote = await db.PersonalNotes.FindAsync(id);
            if(personalNote == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<Note>(personalNote);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> PutPersonalNote(int id, Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id)
            {
                return BadRequest();
            }

            var personalNote = await db.PersonalNotes.FindAsync(id);
            if (personalNote == null)
            {
                return NotFound();
            }

            personalNote.Name = note.Name;
            personalNote.Description = note.Description;
            personalNote.IsDone = note.IsDone;
            personalNote.ReminderDate = note.ReminderDate;
            personalNote.UpdatedOn = DateTime.UtcNow;

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
        [ResponseType(typeof(Note))]
        public async Task<IHttpActionResult> PostPersonalNote(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personalNote = Mapper.Map<PersonalNote>(note);
            personalNote.CreatedBy = "cuongduongduy@sample.com";
            personalNote.CreatedOn = DateTime.UtcNow;
            db.PersonalNotes.Add(personalNote);
            await db.SaveChangesAsync();

            note.Id = personalNote.Id;
            return CreatedAtRoute("GetById", new { id = note.Id }, note);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeletePersonalNote(int id)
        {
            var personalnote = await db.PersonalNotes.FindAsync(id);
            if (personalnote == null)
            {
                return NotFound();
            }

            db.PersonalNotes.Remove(personalnote);
            await db.SaveChangesAsync();

            return Ok();
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