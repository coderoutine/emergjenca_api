using Emergency.DAL.Data.Entities;
using Emergency.DAL.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.DataFilters
{
    public class ContactPersonFilter
    {
 
        public IEnumerable<Guid> Events { get; set; }

        internal Expression<Func<ContactPerson, bool>> Filter => Filter_Func();
        private Expression<Func<ContactPerson, bool>> Filter_Func()
        {
            var predicate = PredicateBuilder.True<ContactPerson>();
            if (Events != null && Events.Any()) predicate = predicate.And(z => Events.Contains(z.EventId));
            return predicate;
        }
    }
}
