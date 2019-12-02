using Emergency.DAL.Data.Enums;
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
        public EventType Type { get; set; }
        public int Severity { get; set; }
        public DateTime? Time { get; set; }
        public bool Verified { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public bool IsStillRelevant { get; set; }
        public virtual ICollection<Shelter> Shelter { get; set; }
        public virtual ICollection<Supplies> Supplies { get; set; }
        public virtual ICollection<Volunteer> Volunteer { get; set; }
    }

 
}
