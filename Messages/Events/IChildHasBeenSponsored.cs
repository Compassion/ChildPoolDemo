using System;

namespace Messages.Events
{
    public interface IChildHasBeenSponsored
    {
        Guid ChildId { get; set; }

        Guid SessionId { get; set; }
    }
}
