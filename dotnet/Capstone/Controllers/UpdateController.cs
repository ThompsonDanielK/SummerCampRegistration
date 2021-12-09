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

        [HttpPut("camper")]
        public ActionResult UpdateCamper(Camper camper)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Camper oldCamperData = camp.FetchCamper(camper.CamperCode);

            int requestId = request.AddNewCamperUpdateRequest(userId, camper, oldCamperData);
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests("camper_updates", requestId);

            return Ok();

        }

        [HttpPut("family")]
        public ActionResult UpdateFamily(Family family)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Family oldFamilyData = camp.FetchFamily(family.FamilyId);

            int requestId = request.AddNewFamilyUpdateRequest(userId, family, oldFamilyData);
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests("family_updates", requestId);

            return Ok();

        }

        [HttpPut("approval/camper/{requestId}")]
        public ActionResult UpdateCamperRequestApproved(int requestId)
        {
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests("camper_updates", requestId);
            return Ok();
        }

        [HttpPut("approval/family/{requestId}")]
        public ActionResult UpdateFamilyRequestApproved(int requestId)
        {
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests("family_updates", requestId);
            return Ok();
        }
    }
}
