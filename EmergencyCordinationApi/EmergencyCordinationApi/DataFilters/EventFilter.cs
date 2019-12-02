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
    public class EventFilter
    {
        public IEnumerable<EventType> Types { get; set; }
        public bool? Verified { get; set; }
        public bool? Relevant { get; set; }
        internal Expression<Func<Event, bool>> Filter => Filter_Func();
        private Expression<Func<Event, bool>> Filter_Func()
        {
            var predicate = PredicateBuilder.True<Event>();
            if (Types != null && Types.Any()) predicate = predicate.And(z => Types.Contains(z.Type));
            if (Verified.HasValue) predicate = predicate.And(z => z.Verified == Verified);
            if (Relevant.HasValue) predicate = predicate.And(z => z.IsStillRelevant == Relevant);
            return predicate;
        }
    }
}
