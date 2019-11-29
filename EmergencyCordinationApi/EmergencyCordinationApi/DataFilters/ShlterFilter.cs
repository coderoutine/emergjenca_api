using Emergency.DAL.Data.Entities;
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
        public int[] Types { get; set; }
        public Guid[] Events { get; set; }

        internal Expression<Func<Shelter, bool>> Filter => Filter_Func();
        private Expression<Func<Shelter, bool>> Filter_Func()
        {
            var predicate = PredicateBuilder.True<Shelter>();
            if (Types != null && Types.Any()) predicate = predicate.And(z => Types.Contains((int)z.Type));
            if (Events != null && Events.Any()) predicate = predicate.And(z => Events.Contains(z.EventId));
            return predicate;
        }
    }
}
