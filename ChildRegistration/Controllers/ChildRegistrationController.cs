using System;
using System.Linq;
using System.Web.Mvc;
using ChildRegistration.Models;
using Messages.Commands;

namespace ChildRegistration.Controllers
{
    public class ChildRegistrationController : Controller
    {
        // GET: /ChildRegistration/
        public ActionResult Index()
        {
            using (var session = MvcApplication.DocumentStore.OpenSession())
            {
                return View(session.Query<Child>());
            }
        }

        // POST: /RegisterNewChild/
        [HttpPost]
        public ActionResult RegisterNewChild(Guid childId, string childName, int childAge, string countryName, string programType, string otherDetailsOnlyThisServiceCaresAbout, string childPhotoLink)
        {
            using (var session = MvcApplication.DocumentStore.OpenSession())
            {
                session.Store(new Child
                {
                    ChildId = childId,
                    ChildName = childName,
                    ChildAge = childAge,
                    CountryName = countryName,
                    ProgramType = programType,
                    OtherDetailsOnlyThisServiceCaresAbout = otherDetailsOnlyThisServiceCaresAbout,
                    ChildPhotoLink = new Uri(childPhotoLink)
                });
                session.SaveChanges();
            }

            MvcApplication.Bus.Send<IChildRegisteredInProgram>(m =>
            {
                m.ChildId = childId;
                m.ChildName = childName;
                m.ChildAge = childAge;
                m.CountryName = countryName;
                m.ProgramType = programType;
                m.OtherChildDetailsThatOurServiceIsNotInterestedIn = otherDetailsOnlyThisServiceCaresAbout;
                m.ChildPhotoLink = new Uri(childPhotoLink);
            });

            return RedirectToAction("Index", "ChildRegistration");
        }

        public ActionResult UnregisterChild(string childId)
        {
            using (var session = MvcApplication.DocumentStore.OpenSession())
            {
                var foundChild = session.Query<Child>()
                    .Where(child => child.ChildId == Guid.Parse(childId))
                    .FirstOrDefault();

                if (foundChild != null)
                    session.Delete(foundChild);

                session.SaveChanges();

                MvcApplication.Bus.Send<ChildHasBeenDeRegistered>(m => m.ChildId = Guid.Parse(childId));
            }

            return RedirectToAction("Index", "ChildRegistration");
        }
    }
}
