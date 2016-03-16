using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using IdentityModel.Client;
using Constants = OAuth2PersonalNotes.Share.Constants;

namespace OAuth2PersonalNotes.Web.Helpers
{
    public class NotesHttpClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(Constants.NotesApi)};

            //var accessToken = RequestAccessTokenAuthorizationCode();
            //if (accessToken != null)
            //{
            //    client.SetBearerToken(accessToken);
            //}   

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static string RequestAccessTokenAuthorizationCode()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get("PersonalNotesCookie");
            if (cookie != null && cookie["access_token"] != null)
            {
                return cookie["access_token"];
            }
            var state = HttpContext.Current.Request.Url.OriginalString;

            var authorizeRequest = new AuthorizeRequest(
                Constants.NotesStsAuthorizationEndpoint);

            var url = authorizeRequest.CreateAuthorizeUrl("personalnotesauthcode", "code", "notesuser",
                Constants.NotesMvcstsCallback, state);

            HttpContext.Current.Response.Redirect(url);

            return null;
        }
    }
}