using System;

namespace ChildPoolListings.Models
{
    public class RegisteredChild
    {
        public Guid ChildId { get; set; }

        public string ChildName { get; set; }

        public int ChildAge { get; set; }

        public string CountryName { get; set; }

        public string ProgramType { get; set; }

        public Uri ChildPhotoLink { get; set; }

        public Guid RequestLockId { get; set; }

        public bool MarkAsDeleted { get; set; }
    }
}