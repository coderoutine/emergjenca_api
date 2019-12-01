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
    public class SupplieBaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Lat { get; set; }
        [Required]
        public string Lng { get; set; }
        [Required]
        public SupplyStatus Status { get; set; }
        public Guid EventId { get; set; }

    }
    public class SuplieCreateViewModel:SupplieBaseModel
    {

      
        public IEnumerable<SupplyContanctPersonCreateModel> ContactPersons { get; set; }

    }
    public class SupplyContanctPersonCreateModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    public class SupplyContanctPersonViewModel : SupplyContanctPersonCreateModel
    {
        public Guid Id { get; set; }
        public Guid SupplyId { get; set; }
    }
    public class SupplyViewModel: SupplieBaseModel
    {
        public Guid Id { get; set; }
        public  IEnumerable<SupplyContanctPersonViewModel> ContactPersons { get; set; }
        public bool CanEdit { get; private set; }
        public bool CanDelete { get; private set; }

        public static Expression<Func<Supplies, SupplyViewModel>> Map(Guid? tenantId) {

            return (s) => new SupplyViewModel
            {
                Id = s.Id,
                CanEdit = tenantId == s.TenantId,
                CanDelete = tenantId == s.TenantId,
                Address = s.Address,
                City = s.City,
                Country = s.Country,
                Description = s.Description,
                Lat = s.Lat,
                Lng = s.Lng,
                Name = s.Name,
                Status = s.Status,
                EventId = s.EventId,
                ContactPersons = s.ContactPerson.Select(cp => new SupplyContanctPersonViewModel
                {
                    Id = cp.Id,
                    Email = cp.Email,
                    FirstName = cp.FirstName,
                    LastName = cp.LastName,
                    Phone = cp.Phone,
                    SupplyId = cp.SupplieId.Value
                })
            };
        }
    }

}
