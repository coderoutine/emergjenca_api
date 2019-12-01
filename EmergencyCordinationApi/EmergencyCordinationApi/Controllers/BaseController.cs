﻿using Microsoft.AspNetCore.Http;
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
        public Guid CurrentUserId => Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }

