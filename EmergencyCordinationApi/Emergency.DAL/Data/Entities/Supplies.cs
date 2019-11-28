using System;
using System.Collections.Generic;

namespace Emergency.DAL.Data.Entities
{
    public partial class Supplies
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public Guid ContactPersonId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public Guid EventId { get; set; }

        public virtual ContactPerson ContactPerson { get; set; }
        public virtual Event Event { get; set; }
    }
}
