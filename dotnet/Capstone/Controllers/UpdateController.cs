using Capstone.Models;
using Capstone.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class UpdateController : ControllerBase
    {
        private readonly IUpdatesDao updates;
        private readonly IRequestDao request;
        private readonly ICampDao camp;

        public UpdateController(ICampDao camp, IUpdatesDao updates, IRequestDao request)
        {
            this.camp = camp;
            this.updates = updates;
            this.request = request;
        }

        [HttpGet("camperUpdateList")]
        public ActionResult CamperUpdateList()
        {
            List<Update> updateList = request.GetCamperUpdateList(true);

            return Ok(updateList);
        }

        [HttpGet("familyUpdateList")]
        public ActionResult FamilyUpdateList()
        {
            List<Update> updateList = request.GetCamperUpdateList(false);

            return Ok(updateList);
        }

        [HttpPost("camper")]
        public ActionResult UpdateCamper(Camper camper)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Camper oldCamperData = camp.FetchCamper(camper.CamperCode);

            int requestId = request.AddNewCamperUpdateRequest(userId, camper, oldCamperData);
            if (requestId == -1)
            {
                return BadRequest("Problem creating update in database");
            }
            if (this.User.IsInRole("admin"))
            { 
                updates.ProcessApprovedRequests("camper_updates", requestId);
            } 
            return Ok(requestId);
        }

        [HttpPost("family")]
        public ActionResult UpdateFamily(Family family)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Family oldFamilyData = camp.FetchFamily(family.FamilyId);

            int requestId = request.AddNewFamilyUpdateRequest(userId, family, oldFamilyData);
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests("family_updates", requestId);

            return Ok(requestId);

        }

        [HttpPut("approval/camper/{requestId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCamperRequestApproved(int requestId)
        {
            updates.ProcessApprovedRequests("camper_updates", requestId);
            return Ok();
        }

        [HttpPut("approval/family/{requestId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateFamilyRequestApproved(int requestId)
        {
            updates.ProcessApprovedRequests("family_updates", requestId);
            return Ok();
        }
        [HttpPut("rejection/camper/{requestId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCamperRequestRejected(int requestId)
        {
            updates.FinalizeRequest("camper_updates", requestId, "Rejected");
            return Ok();
        }

        [HttpPut("rejection/family/{requestId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateFamilyRequestRejected(int requestId)
        {
            updates.FinalizeRequest("family_updates", requestId, "Rejected");
            return Ok();
        }

        [HttpGet("camper/{camperCode}")]
        public ActionResult GetUpdatesByCamperCode(int camperCode)
        {
            List<Update> updates = request.GetCamperUpdatesByCamperCode(camperCode);

            if (updates != null)
            {
                return Ok(updates);
            }
            return BadRequest();
        }
    }
}
