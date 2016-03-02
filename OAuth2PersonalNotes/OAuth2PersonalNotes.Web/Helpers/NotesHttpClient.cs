using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Constants = OAuth2PersonalNotes.Share.Constants;

namespace OAuth2PersonalNotes.Web.Helpers
{
    public class NotesHttpClient
    {
        public static HttpClient GetClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(Constants.NotesApi)};


            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}