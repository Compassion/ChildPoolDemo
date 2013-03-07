using System;

namespace Messages.Commands
{
    public class RegisterIntentToSponsor
    {
        /// <summary>
        /// Gets or sets the Child's Id
        /// </summary>
        public Guid ChildId { get; set; }

        public Guid SessionId { get; set; }
    }
}
