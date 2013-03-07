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
        IHandleMessages<RegisterIntentToSponsor>,
        IHandleTimeouts<IntentToSponsorTimeOut>,
        IHandleMessages<VerifyIntentToSponsor>,
        IHandleMessages<IChildHasBeenDeRegistered>,
        IHandleTimeouts<DeleteChildRegistration>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<RegisterIntentToSponsor>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<IntentToSponsorTimeOut>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<VerifyIntentToSponsor>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<IChildHasBeenDeRegistered>(s => s.ChildId, m => m.ChildId);
            ConfigureMapping<DeleteChildRegistration>(s => s.ChildId, m => m.ChildId);
        }

        public void Handle(IChildRegisteredInProgram message)
        {
            Console.WriteLine("Informed of Child Registration. Adding {0} to Child Pool.", message.ChildName);

            // Only store the data that this service requires.
            Data.ChildId = message.ChildId;
            Data.ChildName = message.ChildName;
            Data.ChildAge = message.ChildAge;
            Data.CountryName = message.CountryName;
            Data.ProgramType = message.ProgramType;
            Data.ChildPhotoLink = message.ChildPhotoLink;
        }

        public void Handle(RegisterIntentToSponsor message)
        {
            Console.WriteLine("Session {0} has requested sponsorship of child {1}", message.SessionId, message.ChildId);
            
            // Check if marked for deletion
            if (Data.MarkAsDeleted)
            {
                Bus.Reply<ChildRequestResponse>(m =>
                {
                    m.Successful = false;
                    m.Reason = "This child has been de-registered.";
                });
                return;
            }

            // Check if anyone else is intending to sponsor this child.
            if (Data.RequestLockId != Guid.Empty)
            {
                Bus.Reply<ChildRequestResponse>(m =>
                { 
                    m.Successful = false;
                    m.Reason = "This child is pending sponsorship by someone else.";
                });
                return;
            }

            // Place Lock on Data
            Data.RequestLockId = message.SessionId;

            RequestUtcTimeout<IntentToSponsorTimeOut>(TimeSpan.FromMinutes(10), t => t.ChildId = Data.ChildId);

            Bus.Reply<ChildRequestResponse>(m =>
            {
                m.Successful = true;
                m.Reason = "This child is now locked for sponsorship pending verification.";
            });
        }

        public void Timeout(IntentToSponsorTimeOut state)
        {
            Console.WriteLine("Intent to sponsor child {0} has expired for session {1}", Data.ChildId, Data.RequestLockId);
            Data.RequestLockId = Guid.Empty;
        }

        public void Handle(VerifyIntentToSponsor message)
        {
            Console.WriteLine("Session {0} has verified sponsorship of child {1}", message.SessionId, message.ChildId);

            // Check if marked for deletion
            if (Data.MarkAsDeleted)
            {
                Bus.Reply<ChildRequestResponse>(m =>
                {
                    m.Successful = false;
                    m.Reason = "This child has been de-registered.";
                });
                return;
            }

            // Check if anyone else is intending to sponsor this child.
            if (Data.RequestLockId != message.SessionId)
            {
                Bus.Reply<ChildRequestResponse>(m =>
                {
                    m.Successful = false;
                    m.Reason = "Cannot Verify sponsorship for this child as someone else already has preference.";
                });
                return;
            }

            Bus.Publish<IChildHasBeenSponsored>(m =>
            {
                m.ChildId = Data.ChildId;
                m.SessionId = Data.RequestLockId;
            });

            Bus.Reply<ChildRequestResponse>(m =>
            {
                m.Successful = true;
                m.Reason = "Child has been sponsored.";
            });

            MarkAsComplete();
        }

        public void Handle(IChildHasBeenDeRegistered message)
        {
            Console.WriteLine("Child {0} has been de-registered. Giving opportunity for better messages by using timeout.", message.ChildId);
            Data.MarkAsDeleted = true;
            RequestUtcTimeout<DeleteChildRegistration>(TimeSpan.FromMinutes(10), t => t.ChildId = Data.ChildId);
        }

        public void Timeout(DeleteChildRegistration state)
        {
            Console.WriteLine("Record for child {0} has been deleted.", Data.ChildId);
            MarkAsComplete();
        }
    }
}
