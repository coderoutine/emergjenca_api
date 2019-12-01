using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.Controllers
{
    public class BaseController : ControllerBase
    {
        public Guid? CurrentUserId
        {
            get
            {
                var parsed = Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
                return parsed ? (Guid?)userId : null;

            }
        }
    }
}
    

