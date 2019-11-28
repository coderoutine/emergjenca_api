using EmergencyCordinationApi.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EmergencyCordinationApi.Data
{
    public class IdentityDBContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public IdentityDBContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
