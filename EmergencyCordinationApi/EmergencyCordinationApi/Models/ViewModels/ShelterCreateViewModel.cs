using Emergency.DAL.Data.Entities;
using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.Models.ViewModels
{
   public class ShelterBaseModel
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
    }
    public class ShelterCreateViewModel:ShelterBaseModel
    {
       
        public IEnumerable<ShelterContanctPersonCreateModel> ContactPersons { get; set; }

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
    public class ShelterContactPersonViewModel : ShelterContanctPersonCreateModel
    {
        public Guid Id { get; set; }
        public Guid ShelterId { get; set; }
    }
    public class ShelterViewModel : ShelterBaseModel
    {
        public Guid Id { get; set; }
        public  IEnumerable<ShelterContactPersonViewModel> ContactPersons { get; set; }
        public bool CanEdit { get; private set; }
        public bool CanDelete { get; private set; }

        public static Expression<Func<Shelter, ShelterViewModel>> Map(Guid? tenantId) 
        {
          return (s)=> new ShelterViewModel
            {
                Id = s.Id,
                CanEdit= tenantId==s.TenantId,
                CanDelete= tenantId==s.TenantId,
              Address = s.Address,
                Capacity = s.Capacity,
                City = s.City,
                Country = s.Country,
                Description = s.Description,
                Lat = s.Lat,
                Lng = s.Lng,
                Name = s.Name,
                Type = s.Type,
                EventId = s.EventId,
                ContactPersons = s.ContactPerson.Select(cp => new ShelterContactPersonViewModel
                {
                    Id = cp.Id,
                    Email = cp.Email,
                    FirstName = cp.FirstName,
                    LastName = cp.LastName,
                    Phone = cp.Phone,
                    ShelterId = cp.ShelterId.Value
                })
            };
        }

    }
}

