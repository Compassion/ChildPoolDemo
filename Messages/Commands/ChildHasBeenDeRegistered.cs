using System;

namespace Messages.Commands
{
    public interface ChildHasBeenDeRegistered
    {
        Guid ChildId { get; set; }
    }
}
