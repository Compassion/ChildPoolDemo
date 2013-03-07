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
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:59677/ChildListings/GetChildren/").Result;
            var content = response.Content;
            var children = new DataContractJsonSerializer(typeof(IEnumerable<Child>)).ReadObject(content.ReadAsStreamAsync().Result);
            return View(children);
        }

        // GET: /Home/SponsorChild/
        public ActionResult SponsorChild(string childId)
        {
            var sessionId = Guid.NewGuid();
            Session["SessionId"] = sessionId;
            var childIdGuid = Guid.Parse(childId);
            var client = new HttpClient();
            var response = client.GetAsync("http://localhost:59677/ChildListings/GetChild/?childId=" + childIdGuid).Result;
            var content = response.Content;
            var child = new DataContractJsonSerializer(typeof(Child)).ReadObject(content.ReadAsStreamAsync().Result);

            var command = new RegisterIntentToSponsor 
            { 
                ChildId = childIdGuid,
                SessionId = sessionId 
            };

            var successful = MvcApplication.Bus.Send(command)
                .Register(CommandResponseCallback, this)
                .AsyncWaitHandle.WaitOne(5000);

            if (!successful)
            {
                ViewBag.Successful = false;
                ViewBag.ErrorReason = "Timed Out";
            }

            return View(child);
        }

        public ActionResult ConfirmSponsorship(string childId)
        {
            var sessionId = (Guid)Session["SessionId"];
            var childIdGuid = Guid.Parse(childId);

            var command = new VerifyIntentToSponsor
            {
                ChildId = childIdGuid, 
                SessionId = sessionId
            };

            var successful = MvcApplication.Bus.Send(command)
                .Register(CommandResponseCallback, this)
                .AsyncWaitHandle.WaitOne(5000);

            if (!successful)
            {
                ViewBag.Successful = false;
                ViewBag.ErrorReason = "Timed Out";
            }

            return View("ConfirmSponsorship");
        }

        private void CommandResponseCallback(IAsyncResult asyncResult)
        {
            var result = (CompletionResult)asyncResult.AsyncState;
            var controller = (HomeController)result.State;
            var response = result.Messages.Cast<ChildRequestResponse>().First();
            controller.ViewBag.ErrorReason = response.Reason;
            controller.ViewBag.Successful = response.Successful;
        }
    }
}
