using System.Reflection;

using NServiceBus;

namespace Configuration
{
    public static class MessageConventions
    {
        /// <summary>
        /// Defines the conventions to determine what constitutes a Message, Command, or an Event.
        /// Messages are defined as classes or interfaces found in a namespace ending with Requests, Responses, or Timeouts.
        /// Commands are defined as classes or interfaces found in a namespace ending with Commands.
        /// Events are defined as classes or interfaces found in a namespace ending with Events.
        /// </summary>
        /// <param name="configure">The NServiceBus Configure object.</param>
        /// <param name="endpointName">The name that the endpoint will be called.</param>
        /// <returns>The NServiceBus Configure object with the Conventions set.</returns>
        public static Configure DefineMessageConventions(this Configure configure, string endpointName)
        {
            return configure
                .DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.EndsWith("Requests") || t.Namespace.EndsWith("Responses") || t.Namespace.EndsWith("Timeouts")))
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith("Commands"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith("Events"))
                .DefineEndpointName(() => endpointName);
        }
    }
}
