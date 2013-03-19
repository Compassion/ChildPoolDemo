using System.Web.Mvc;
using System.Web.Routing;

using Configuration;

using NServiceBus;

namespace SponsorAChild
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IBus Bus { get; private set; }

        protected void Application_Start()
        {
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
    }
}