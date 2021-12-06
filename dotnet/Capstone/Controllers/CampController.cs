using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CampController : ControllerBase
    {
        private readonly ICampDao camp;

        public CampController(ICampDao camp)
        {
            this.camp = camp;
        }

        [HttpGet("/CamperList")]
        public ActionResult GetCamperList()
        {
            List<Camper> camperList = camp.FetchAllCampers();

            return Ok(camperList);
        }

        [HttpPost("Camper")]
        public ActionResult PostCamper(Camper camper)
        {
            if (camp.AddCamper(camper))
            {
                return Created("Your camper has been added", camper);
            }

            return BadRequest(new { message = "This request could not be completed." });
        }

    }
}
