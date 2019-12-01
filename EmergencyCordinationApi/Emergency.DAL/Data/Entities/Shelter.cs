using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;

namespace Emergency.DAL.Data.Entities
{
    public partial class Shelter
    {
        public Shelter()
        {
            ContactPerson = new HashSet<ContactPerson>();
        }

        public Guid Id { get; set; }
        public ShelterType Type { get; set; }
        public int? Capacity { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid TenantId { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
        public virtual ICollection<ContactPerson> ContactPerson { get; set; }
    }
}
