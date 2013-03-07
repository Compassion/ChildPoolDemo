using System;

namespace Messages.Commands
{
    /// <summary>
    /// This event notifies that a Child has been Registered in a program and is published from the ChildRegistration Service
    /// </summary>
    public interface IChildRegisteredInProgram
    {
        Guid ChildId { get; set; }

        string ChildName { get; set; }

        int ChildAge { get; set; }

        string CountryName { get; set; }

        string ProgramType { get; set; }

        Uri ChildPhotoLink { get; set; }

        string OtherChildDetailsThatOurServiceIsNotInterestedIn { get; set; }
    }
}
