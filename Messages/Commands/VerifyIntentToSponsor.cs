using System;

namespace Messages.Commands
{
    public class VerifyIntentToSponsor
    {
        public Guid ChildId { get; set; }

        public Guid SessionId { get; set; }
    }
}