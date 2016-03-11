using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using OAuth2PersonalNotes.DTO;
using OAuth2PersonalNotes.Web.Helpers;

namespace OAuth2PersonalNotes.Web.Controllers
{
    public class NotesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var httpClient = NotesHttpClient.GetClient();

            var response = await httpClient.GetAsync("api/notes").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var notes = JsonConvert.DeserializeObject<IList<Note>>(stringContent).OrderBy(x=>x.ReminderDate);

                return View(notes);
            }
            return View("Error",
                new HandleErrorInfo(ExceptionHelper.GetExceptionFromResponse(response),
                    "Notes", "Index"));
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Note note)
        {
            if (!ModelState.IsValid)
            {
                return View("BadRequest");
            }
            var httpClient = NotesHttpClient.GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(note), System.Text.Encoding.Unicode,
                "application/json");

            var reponse = await httpClient.PostAsync("api/notes", stringContent).ConfigureAwait(false);

            if (reponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Error",
                new HandleErrorInfo(ExceptionHelper.GetExceptionFromResponse(reponse),
                    "Notes", "Index"));

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var httpClient = NotesHttpClient.GetClient();

            var reponse = await httpClient.GetAsync(string.Format("api/notes/{0}", id)).ConfigureAwait(false);

            if (reponse.IsSuccessStatusCode)
            {
                var stringContent = await reponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                var note = JsonConvert.DeserializeObject<EditNote>(stringContent);

                return View(note);
            }
            return View("Error",
                new HandleErrorInfo(ExceptionHelper.GetExceptionFromResponse(reponse),
                    "Notes", "Index"));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, EditNote note)
        {
            var httpClient = NotesHttpClient.GetClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(note), System.Text.Encoding.Unicode,
                "application/json");
            var reponse =
                await httpClient.PutAsync(string.Format("api/notes/{0}", id), stringContent).ConfigureAwait(false);


            if (reponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Error",
                new HandleErrorInfo(ExceptionHelper.GetExceptionFromResponse(reponse),
                    "Notes", "Index"));
        }

        public async Task<ActionResult> MakeNoteDone(int id)
        {
            //Todo: shoud apply patch later
            var httpClient = NotesHttpClient.GetClient();

            var reponse = await httpClient.GetAsync(string.Format("api/notes/{0}", id)).ConfigureAwait(false);

            if (reponse.IsSuccessStatusCode)
            {
                var stringContent = await reponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                var note = JsonConvert.DeserializeObject<EditNote>(stringContent);
                note.IsDone = true;

                var putStringContent = new StringContent(JsonConvert.SerializeObject(note), System.Text.Encoding.Unicode,
                    "application/json");
                reponse =
                    await
                        httpClient.PutAsync(string.Format("api/notes/{0}", id), putStringContent).ConfigureAwait(false);

                if (reponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error",
                new HandleErrorInfo(ExceptionHelper.GetExceptionFromResponse(reponse),
                    "Notes", "Index"));
        }
    }
}