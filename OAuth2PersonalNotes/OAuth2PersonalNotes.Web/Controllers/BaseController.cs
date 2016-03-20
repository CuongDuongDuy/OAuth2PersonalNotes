﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using IdentityModel;
using OAuth2PersonalNotes.Share;
using OAuth2PersonalNotes.Web.AuthorizationMiddleware.Models;
using OAuth2PersonalNotes.Web.Models;

namespace OAuth2PersonalNotes.Web.Controllers
{
    [Authorize]
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
                        Email = claimIdentity?.FindFirst(JwtClaimTypes.Email) == null ? "" : claimIdentity.FindFirst(JwtClaimTypes.Email).Value,
                        FirstName =  claimIdentity?.FindFirst(JwtClaimTypes.GivenName) == null ? "" : claimIdentity.FindFirst(JwtClaimTypes.GivenName).Value,
                        LastName =  claimIdentity?.FindFirst(JwtClaimTypes.FamilyName) == null ? "" : claimIdentity.FindFirst(JwtClaimTypes.FamilyName).Value,
                        AccessToken = claimIdentity?.FindFirst("access_token") == null ? "" : claimIdentity.FindFirst("access_token").Value,
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