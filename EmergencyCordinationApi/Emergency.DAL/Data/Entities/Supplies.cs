﻿using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;

namespace Emergency.DAL.Data.Entities
{
    public partial class Supplies
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public SupplyStatus Status { get; set; }
        public Guid EventId { get; set; }

        public Guid TenantId { get; set; }

        public virtual ICollection<ContactPerson> ContactPerson { get; set; }
        public virtual Event Event { get; set; }
    }
}
