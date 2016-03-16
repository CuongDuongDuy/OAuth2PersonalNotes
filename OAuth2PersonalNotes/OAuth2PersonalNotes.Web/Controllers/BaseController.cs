using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using OAuth2PersonalNotes.Web.Models;

namespace OAuth2PersonalNotes.Web.Controllers
{
    public class BaseController : Controller
    {
        private CurrentUserInfo currentUser;

        protected CurrentUserInfo CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    var claimIdentity = (User.Identity) as ClaimsIdentity;
                    currentUser = new CurrentUserInfo
                    {
                        Email = claimIdentity == null || claimIdentity.FindFirst("email") == null ? "" : claimIdentity.FindFirst("email").Value,
                        FirstName =  claimIdentity == null || claimIdentity.FindFirst("given_name") == null ? "" : claimIdentity.FindFirst("given_name").Value,
                        LastName =  claimIdentity == null || claimIdentity.FindFirst("family_name") == null ? "" : claimIdentity.FindFirst("family_name").Value
                    };
                }
                return currentUser;
            }
        }
    }
}