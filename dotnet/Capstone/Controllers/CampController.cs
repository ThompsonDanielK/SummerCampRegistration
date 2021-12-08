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

        [HttpGet("Camper/{camperCode}")]
        public ActionResult GetCamper(int camperCode)
        {
            Camper camper = camp.FetchCamper(camperCode);

            return Ok(camper);
        }

        [HttpGet("family/{familyId}")]
        public ActionResult GetFamily(int familyId)
        {
            Family family = camp.FetchFamily(familyId);

            return Ok(family);
        }

        [HttpPost("Camper")]
        public ActionResult PostCamperAndFamily(CamperFamily camperFamily)
        {
            bool familyExists = false;
            bool familyCreationSuccess = true;
            int familyId = 0;
            string successMessage = "Your camper has been added";

            if (camperFamily.Family.FamilyId == 0)
            {
                familyId = camp.AddFamily(camperFamily.Family);

                if (!familyId.Equals(null))
                {
                    familyExists = familyCreationSuccess = true;
                }

                if (familyCreationSuccess)
                {
                    successMessage = "Your camper and family has been added";
                }
            }
            else
            {
                familyExists = true;
            }

            if (familyExists || familyCreationSuccess)
            {
                camperFamily.Camper.FamilyId = familyId;

                if (camp.AddCamper(camperFamily.Camper))
                {
                    return Created(successMessage, camperFamily.Camper);
                }
            }

            return BadRequest(new { message = "This request could not be completed." });
        }


    }
}
