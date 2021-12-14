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
        //private readonly IUpdatesDao updates;

        public CampController(ICampDao camp, IUpdatesDao updates)
        {
            this.camp = camp;
            //  this.updates = updates;

        }

        [HttpGet("CamperList")]
        public ActionResult GetCamperList()
        {
            List<Camper> camperList = camp.FetchAllCampers();

            return Ok(camperList);
        }

        [HttpGet("FamilyList")]
        public ActionResult GetFamilyList()
        {
            List<Family> familyList = camp.FetchAllFamilies();

            return Ok(familyList);
        }

        [HttpPost("Camper")]
        public ActionResult PostCamper(Camper camper)
        {
            int camperCode = camp.AddCamper(camper);

            if (camperCode != 0)
            {
                string successMessage = "Your camper has been added";
                return Created(successMessage, camperCode);
            }

            return BadRequest(new { message = "This request could not be completed." });
        }

        [HttpPost("Family")]
        public ActionResult PostFamily(Family family)
        {
            int familyId = camp.AddFamily(family);
            if (familyId > 0)
            {
                string successMessage = "Your family has been added";
                return Created(successMessage, familyId);
            }
            return BadRequest(new { message = "This request could not be completed." });
        }
    }
}
