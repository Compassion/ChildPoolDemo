using Configuration;
using NServiceBus;

namespace ServerEndpoint 
{
    /// <summary>
    /// This Class configures how NServiceBus runs.
    /// </summary>
    public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
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
        }
    }
}