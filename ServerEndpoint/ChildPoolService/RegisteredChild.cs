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
        IHandleMessages<ChildHasBeenDeRegistered>,
        IHandleMessages<RegisterIntentToSponsor>,
        IHandleTimeouts<IntentToSponsorTimeOut>,
        IHandleMessages<VerifyIntentToSponsor>
    {
        public override void ConfigureHowToFindSaga()
        { 
            ConfigureMapping<DeleteChildRegistration>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<RegisterIntentToSponsor>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<IntentToSponsorTimeOut>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<VerifyIntentToSponsor>(s => s.ChildId, m => m.ChildId);
        }        

        public void Handle(IChildRegisteredInProgram message)
        { //todo:demo1       
        }

        public void Handle(ChildHasBeenDeRegistered message)
        { //todo:demo3
        }

        public void Handle(RegisterIntentToSponsor message)
        { //todo:demo4
        }

        public void Timeout(IntentToSponsorTimeOut state)
        { //todo:demo5
        }

        public void Handle(VerifyIntentToSponsor message)
        { //todo:demo6
        }
    }
}
