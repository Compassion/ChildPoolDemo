using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NServiceBus.Persistence.Raven;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace ChildPoolListings
{
    public class WebApiApplication : HttpApplication
    {
        public static IDocumentStore DocumentStore { get; private set; }

        protected void Application_Start()
        {
            InitialiseDatabase();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void InitialiseDatabase()
        {
            DocumentStore = new DocumentStore
            {
                Url = "http://localhost:8080/", 
                DefaultDatabase = "ServerEndpoint",
                Conventions = new DocumentConvention { FindTypeTagName = new RavenConventions().FindTypeTagName },
                ResourceManagerId = Guid.Parse("1249A871-A314-4D3C-A46C-34E02B87C541")
            };
            DocumentStore.Initialize();
            DocumentStore.DatabaseCommands.EnsureDatabaseExists("ChildRegistration");
        }
    }
}