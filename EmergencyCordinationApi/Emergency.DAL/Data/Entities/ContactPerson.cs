using System;
using System.Collections.Generic;

namespace Emergency.DAL.Data.Entities
{
    public partial class ContactPerson
    {
        public ContactPerson()
        {
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid? ShelterId { get; set; }
        public Guid? SupplieId { get; set; }

        public virtual Shelter Shelter { get; set; }
        public virtual Supplies Supplie { get; set; }
    }
}
