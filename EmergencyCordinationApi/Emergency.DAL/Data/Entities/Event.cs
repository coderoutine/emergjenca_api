using System;
using System.Collections.Generic;

namespace Emergency.DAL.Data.Entities
{
    public partial class Event
    {
        public Event()
        {
            Shelter = new HashSet<Shelter>();
            Supplies = new HashSet<Supplies>();
            Volunteer = new HashSet<Volunteer>();
        }

        public Guid Id { get; set; }
        public int Type { get; set; }
        public int Severity { get; set; }
        public DateTime? Time { get; set; }
        public bool Verified { get; set; }
        public string Descrption { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string Location { get; set; }
        public virtual ICollection<Shelter> Shelter { get; set; }
        public virtual ICollection<Supplies> Supplies { get; set; }
        public virtual ICollection<Volunteer> Volunteer { get; set; }
    }

    public enum EventType
    {
        EarthQuake = 1,
        Fire = 2,
        Flood = 3
    }
}
