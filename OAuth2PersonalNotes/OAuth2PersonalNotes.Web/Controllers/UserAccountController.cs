using System.Web;
using System.Web.Mvc;

namespace OAuth2PersonalNotes.Web.Controllers
{
    public class UserAccountController : Controller
    {
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}