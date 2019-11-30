using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.Models.ViewModels
{
    public class ShelterCreateViewModel
    {
        [Required]
        public ShelterType Type { get; set; }
        public int? Capacity { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Lat { get; set; }
        [Required]
        public string Lng { get; set; }
        [Required]
        public Guid EventId { get; set; }
        public ShelterContanctPersonCreateModel[] ContactPersons { get; set; }

    }
    public class ShelterContanctPersonCreateModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

