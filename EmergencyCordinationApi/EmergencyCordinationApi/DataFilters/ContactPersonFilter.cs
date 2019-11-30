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
 
        public IEnumerable<Guid?> Shelters { get; set; }
        public IEnumerable<Guid?> Supplies { get; set; }

        internal Expression<Func<ContactPerson, bool>> Filter => Filter_Func();
        private Expression<Func<ContactPerson, bool>> Filter_Func()
        {
            var predicate = PredicateBuilder.True<ContactPerson>();
            if (Shelters != null && Shelters.Any()) predicate = predicate.And(z => Shelters.Contains(z.ShelterId));
            if (Supplies != null && Supplies.Any()) predicate = predicate.And(z => Supplies.Contains(z.ShelterId));
            return predicate;
        }
    }
}
