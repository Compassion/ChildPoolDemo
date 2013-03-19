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
        { //todo:demo0            
        }        

        public void Handle(IChildRegisteredInProgram message)
        { //todo:demo1       
        }

        public void Handle(IChildHasBeenDeRegistered message)
        { //todo:demo2
        }

        public void Timeout(DeleteChildRegistration state)
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
