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
        private readonly IUpdatesDao updates;
        private readonly int userId;

        public CampController(ICampDao camp, IUpdatesDao updates)
        {
            this.camp = camp;
            this.updates = updates;
            
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

                int camperCode = camp.AddCamper(camperFamily.Camper);

                if (camperCode != 0)
                {

                    return Created(successMessage, camperCode);
                }
            }

            return BadRequest(new { message = "This request could not be completed." });
        }

        [HttpPut("update/camper")]
        public ActionResult UpdateCamper(Camper camper)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Camper oldCamperData = camp.FetchCamper(camper.CamperCode);

            int requestId = updates.AddNewCamperUpdateRequest(userId, camper, oldCamperData);
            updates.ProcessApprovedRequests(requestId);

            return Ok();

        }

        //[HttpPut("update/family")]
        //public ActionResult UpdateFamily(Family family)
        //{
        //    int userId = int.Parse(this.User.FindFirst("sub").Value);

        //    int requestId = updates.AddNewFamilyUpdateRequest(userId, family);
        //    updates.ProcessApprovedRequests(requestId);

        //    return Ok();

        //}
    }
}
