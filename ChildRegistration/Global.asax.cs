using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Configuration;
using NServiceBus;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;

namespace ChildRegistration
{
    public class MvcApplication : HttpApplication
    {
        public static IBus Bus { get; private set; }

        public static IDocumentStore DocumentStore { get; private set; }

        protected void Application_Start()
        {
            InitialiseDatabase();
            InitialiseBus();

            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void InitialiseBus()
        {
            var config = Configure.With()
                .DefaultBuilder()
                .DefineMessageConventions(GetType().Assembly.GetName().Name)
                .Sagas()
                .XmlSerializer()
                .MsmqTransport()
                    .IsTransactional(true)
                    .PurgeOnStartup(false)
                .RavenPersistence()
                    .RavenSagaPersister()
                    .RavenSubscriptionStorage()
                .RunTimeoutManager()
                .UnicastBus()
                    .ImpersonateSender(true);

            Bus = config.CreateBus().Start();
        }

        private void InitialiseDatabase()
        {
            DocumentStore = new DocumentStore
            {
                Url = "http://localhost:8080/", 
                DefaultDatabase = "ChildRegistration",
                ResourceManagerId = Guid.Parse("BF145BD4-EE71-472C-AD5A-249D69B80455")
            };
            DocumentStore.Initialize();
            DocumentStore.DatabaseCommands.EnsureDatabaseExists("ChildRegistration");
        }
    }
}