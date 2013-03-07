using System;

namespace Messages.Commands
{
    public interface IChildHasBeenDeRegistered
    {
        Guid ChildId { get; set; }
    }
}
