using System;
using System.Linq;
using System.Web.Mvc;
using ChildPoolListings.Models;

namespace ChildPoolListings.Controllers
{
    public class ChildListingsController : Controller
    {
        // GET: /ChildListings/GetChildren/
        public JsonResult GetChildren()
        {
            using (var session = WebApiApplication.DocumentStore.OpenSession())
            {
                var results = session.Query<RegisteredChild>()
                    .Where(c => c.MarkAsDeleted == false)
                    .Where(c => c.RequestLockId == Guid.Empty)
                    .ToArray();
                return Json(results, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: /ChildListings/GetChild/
        public JsonResult GetChild(string childId)
        {
            using (var session = WebApiApplication.DocumentStore.OpenSession())
            {
                var result = session.Query<RegisteredChild>().FirstOrDefault(c => c.ChildId == Guid.Parse(childId));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
