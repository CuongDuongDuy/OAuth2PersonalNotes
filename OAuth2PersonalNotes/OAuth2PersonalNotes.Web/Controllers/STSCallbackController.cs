using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityModel.Client;
using OAuth2PersonalNotes.Share;

namespace OAuth2PersonalNotes.Web.Controllers
{
    public class StsCallbackController : Controller
    {
        // GET: STSCallback
        public async Task<ActionResult> Index()
        {                         
            // get the authorization code from the query string
            var authCode = Request.QueryString["code"];

            // with the auth code, we can request an access token.
            var client = new TokenClient(
                Constants.NotesStsRevokeTokenEndpoint,
                "personalnotesauthcode",
                 Constants.NotesClientSecret); 

            var tokenResponse = await client.RequestAuthorizationCodeAsync(
                authCode,
                Constants.NotesMvcstsCallback);
                    
            // we save the token in a cookie for use later on
            Response.Cookies["PersonalNotesCookie"]["access_token"] = tokenResponse.AccessToken;

            // get the state (uri to return to)
            var state = Request.QueryString["state"];
         
            // redirect to the URI saved in state            
            return Redirect(state);         
        }
    }
}