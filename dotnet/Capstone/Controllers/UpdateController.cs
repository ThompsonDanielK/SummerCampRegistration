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
        public ActionResult UpdateCamper(Update update)
        {
            int requestId = request.AddNewCamperUpdateRequest(update);
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

        [HttpPost("notes")]
        public ActionResult UpdateNotes(Update update)
        {
            int requestId = request.AddNewNoteUpdateRequest(update);
            if (requestId == -1)
            {
                return BadRequest("Problem creating update in database");
            }
            if (this.User.IsInRole("admin"))
            {
                updates.ProcessApprovedRequests("ad_hoc_notes", requestId);
            }
            return Ok(requestId);
        }

        [HttpPost("family")]
        public ActionResult UpdateFamily(Update update)
        {
            int requestId = request.AddNewFamilyUpdateRequest(update);
            if (requestId == -1)
            {
                return BadRequest("Problem creating update in database");
            }
            if (this.User.IsInRole("admin"))
            {
                updates.ProcessApprovedRequests("family_updates", requestId);
            }
            return Ok(requestId);

        }

        [HttpPut("approval/camper")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCamperRequestApproved(int[] requestIds)
        {
            foreach (int id in requestIds)
            {
                updates.ProcessApprovedRequests("camper_updates", id);
            }
            return Ok();
        }

        [HttpPut("approval/family")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateFamilyRequestApproved(int[] requestIds)
        {
            foreach (int id in requestIds)
            {
                updates.ProcessApprovedRequests("family_updates", id);
            }
                return Ok();
        }

        [HttpPut("rejection/camper/{requestId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCamperRequestRejected(int requestId)
        {
            updates.FinalizeRequest("camper_updates", requestId, "Rejected");
            return Ok();
        }

        [HttpPut("rejection/camper")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCamperRequestsRejected(int[] requestIds)
        {
            foreach (int id in requestIds)
            {
                updates.FinalizeRequest("camper_updates", id, "Rejected");
            }
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

        [HttpGet("family/{familyId}")]
        public ActionResult GetUpdatesByFamilyId(int familyId)
        {
            List<Update> updates = request.GetFamilyUpdatesByFamilyId(familyId);

            if (updates != null)
            {
                return Ok(updates);
            }
            return BadRequest();
        }
    }
}
