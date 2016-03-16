using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityModel.Client;
using OAuth2PersonalNotes.Share;

namespace OAuth2PersonalNotes.Web.Controllers
{
    public class StsCallbackController : Controller
    {
        public async Task<ActionResult> Index()
        {                         
            var authCode = Request.QueryString["code"];

            var client = new TokenClient(
                Constants.NotesStsTokenEndpoint,
                "personalnotesauthcode",
                 Constants.NotesClientSecret); 

            var tokenResponse = await client.RequestAuthorizationCodeAsync(
                authCode,
                Constants.NotesMvcstsCallback);
                    
            Response.Cookies["PersonalNotesCookie"]["access_token"] = tokenResponse.AccessToken;

            var state = Request.QueryString["state"];
                 
            return Redirect(state);         
        }
    }
}