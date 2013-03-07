using System;
using Messages.Commands;
using Messages.Events;
using Messages.Responses;
using NServiceBus;
using NServiceBus.Saga;
using ServerEndpoint.ChildPoolService.Timeouts;

namespace ServerEndpoint.ChildPoolService
{
    public class RegisteredChild : Saga<RegisteredChildData>,
        IAmStartedByMessages<IChildRegisteredInProgram>,
        IHandleMessages<IChildHasBeenDeRegistered>,
        IHandleTimeouts<DeleteChildRegistration>,
        IHandleMessages<RegisterIntentToSponsor>,
        IHandleTimeouts<IntentToSponsorTimeOut>,
        IHandleMessages<VerifyIntentToSponsor>
    {
        public override void ConfigureHowToFindSaga()
        {
            // Message Correlation
        }

        public void Handle(IChildRegisteredInProgram message)
        {
        }

        public void Handle(IChildHasBeenDeRegistered message)
        {
        }

        public void Timeout(DeleteChildRegistration state)
        {
        }

        public void Handle(RegisterIntentToSponsor message)
        {
        }

        public void Timeout(IntentToSponsorTimeOut state)
        {
        }

        public void Handle(VerifyIntentToSponsor message)
        {
        }
    }
}
