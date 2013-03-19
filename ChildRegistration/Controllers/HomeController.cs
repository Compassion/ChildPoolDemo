using System.Web.Mvc;

namespace ChildRegistration.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "ChildRegistration");
        }
    }
}
