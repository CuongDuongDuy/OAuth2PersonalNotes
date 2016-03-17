using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Mvc;
using OAuth2PersonalNotes.Share;
using OAuth2PersonalNotes.Web.Models;

namespace OAuth2PersonalNotes.Web.Controllers
{
    public class BaseController : Controller
    {
        private DelegatedUser delegatedUser;

        protected DelegatedUser DelegatedUser
        {
            get
            {
                if (delegatedUser == null)
                {
                    var claimIdentity = (User.Identity) as ClaimsIdentity;
                    delegatedUser = new DelegatedUser
                    {
                        Email = claimIdentity == null || claimIdentity.FindFirst("email") == null ? "" : claimIdentity.FindFirst("email").Value,
                        FirstName =  claimIdentity == null || claimIdentity.FindFirst("given_name") == null ? "" : claimIdentity.FindFirst("given_name").Value,
                        LastName =  claimIdentity == null || claimIdentity.FindFirst("family_name") == null ? "" : claimIdentity.FindFirst("family_name").Value,
                        AccessToken = claimIdentity == null || claimIdentity.FindFirst("access_token") == null ? "" : claimIdentity.FindFirst("access_token").Value,
                    };
                }
                return delegatedUser;
            }
        }

        public HttpClient SecuredNotesClient
        {
            get
            {
                var client = new HttpClient {BaseAddress = new Uri(Constants.NotesApi)};

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                client.SetBearerToken(DelegatedUser.AccessToken);
                return client;
            }
        }
    }
}