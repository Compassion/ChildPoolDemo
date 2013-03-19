using System;

namespace SponsorAChild.Models
{
    public class Child
    {
        public Guid ChildId { get; set; }

        public string ChildName { get; set; }

        public int ChildAge { get; set; }

        public string CountryName { get; set; }

        public string ProgramType { get; set; }

        public Uri ChildPhotoLink { get; set; }
    }
}