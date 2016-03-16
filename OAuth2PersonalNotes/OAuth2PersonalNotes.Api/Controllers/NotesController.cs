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
    [RoutePrefix("api/Notes")]
    public class NotesController : ApiController
    {
        private readonly NotesDbContext db = new NotesDbContext();

        public NotesController()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PersonalNote, NoteDto>();
                cfg.CreateMap<NoteDto, PersonalNote>();
            });
        }

        [HttpGet]
        [Route("")]
        public IQueryable<NoteDto> GetPersonalNotes()
        {
            return db.PersonalNotes.ProjectTo<NoteDto>(Mapper.Configuration);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetById")]
        [ResponseType(typeof(NoteDto))]
        public async Task<IHttpActionResult> GetPersonalNote(int id)
        {
            var personalNote = await db.PersonalNotes.FindAsync(id);
            if(personalNote == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<NoteDto>(personalNote);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> PutPersonalNote(int id, NoteDto noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noteDto.Id)
            {
                return BadRequest();
            }

            var personalNote = await db.PersonalNotes.FindAsync(id);
            if (personalNote == null)
            {
                return NotFound();
            }

            personalNote.Name = noteDto.Name;
            personalNote.Description = noteDto.Description;
            personalNote.IsDone = noteDto.IsDone;
            personalNote.ReminderDate = noteDto.ReminderDate;
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
        [ResponseType(typeof(NoteDto))]
        public async Task<IHttpActionResult> PostPersonalNote(NoteDto noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personalNote = Mapper.Map<PersonalNote>(noteDto);
            personalNote.CreatedOn = DateTime.UtcNow;
            db.PersonalNotes.Add(personalNote);
            await db.SaveChangesAsync();

            noteDto.Id = personalNote.Id;
            return CreatedAtRoute("GetById", new { id = noteDto.Id }, noteDto);
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