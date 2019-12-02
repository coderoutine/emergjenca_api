using Emergency.DAL.Data.Entities;
using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.Models.ViewModels
{
    public class EventListViewModel
    {
        public Guid Id { get; set; }
        public EventType Type { get; set; }
        public int Severity { get; set; }
        public DateTime? Time { get; set; }
        public bool IsStillRelevant { get; set; }
        public bool Verified { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public static Expression<Func<Event, EventListViewModel>> Map => (e) => new EventListViewModel
        {
            Id=e.Id,
            Type=e.Type,
            Severity=e.Severity,
            Time=e.Time,
            Verified=e.Verified,
            Description=e.Description,
            Country=e.Country,
            City=e.City,
            Lat=e.Lat,
            Lng=e.Lng,
            IsStillRelevant=e.IsStillRelevant
        };
    }
    public class EventDetailsViewModel
    {
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
        public int TotalSupplies { get; set; }
        public int TotalSafeZones { get; set; }
        public int TotalShelters { get; set; }
        public static Expression<Func<Event, EventDetailsViewModel>> Map => (e) => new EventDetailsViewModel
        {
            Id = e.Id,
            Type = e.Type,
            Severity = e.Severity,
            Time = e.Time,
            Verified = e.Verified,
            Description = e.Description,
            Country = e.Country,
            City = e.City,
            Lat = e.Lat,
            Lng = e.Lng,
            TotalShelters= e.Shelter.Count(z=>z.Type== ShelterType.Shelter),
            TotalSafeZones= e.Shelter.Count(z=>z.Type== ShelterType.SafeZone),
            TotalSupplies= e.Supplies.Count()
        };
    }

}
