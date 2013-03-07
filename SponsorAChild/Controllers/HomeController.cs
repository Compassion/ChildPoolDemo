using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;
using Messages.Commands;
using Messages.Responses;
using NServiceBus;
using SponsorAChild.Models;

namespace SponsorAChild.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET: /Home/SponsorChild/
        public ActionResult SponsorChild(string childId)
        {
            throw new NotImplementedException();
        }

        public ActionResult ConfirmSponsorship(string childId)
        {
            throw new NotImplementedException();
        }

        private void CommandResponseCallback(IAsyncResult asyncResult)
        {
        }
    }
}
