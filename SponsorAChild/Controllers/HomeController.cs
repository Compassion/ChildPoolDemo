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
        {//todo:demo9
            throw new NotImplementedException("demo9");
        }

        // GET: /Home/SponsorChild/
        public ActionResult SponsorChild(string childId)
        {//todo:demo10
            throw new NotImplementedException("demo10");
        }

        public ActionResult ConfirmSponsorship(string childId)
        {//todo:demo12
            throw new NotImplementedException("demo12");

        }

        private void CommandResponseCallback(IAsyncResult asyncResult)
        {//todo:demo11            
        }
    }
}
