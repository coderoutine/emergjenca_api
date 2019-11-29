using Emergency.DAL.Data.Entities;
using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.DataFilters
{
    public class ShelterFilter
    {
        public IEnumerable<ShelterType> Types { get; set; }
        public IEnumerable<Guid> Events { get; set; }

        internal Expression<Func<Shelter, bool>> Filter => Filter_Func();
        private Expression<Func<Shelter, bool>> Filter_Func()
        {
            var predicate = PredicateBuilder.True<Shelter>();
            if (Types != null && Types.Any()) predicate = predicate.And(z => Types.Contains(z.Type));
            if (Events != null && Events.Any()) predicate = predicate.And(z => Events.Contains(z.EventId));
            return predicate;
        }
    }
}
