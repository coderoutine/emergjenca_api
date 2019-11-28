using System;
using System.Collections.Generic;

namespace Emergency.DAL.Data.Entities
{
    public partial class Volunteer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid EventId { get; set; }
        public int Status { get; set; }

        public virtual Event Event { get; set; }
    }
}
