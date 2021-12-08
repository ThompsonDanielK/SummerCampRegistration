using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("camp/")]
    [ApiController]
    [Authorize]
    public class UpdateController : ControllerBase
    {
        public ActionResult PostFamilyUpdateRequest(Family updatedFamily)
        {
            return Ok();
        }
    }
}
