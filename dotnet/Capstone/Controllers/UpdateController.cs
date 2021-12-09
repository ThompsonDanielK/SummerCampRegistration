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

        [HttpPut("camper")]
        public ActionResult UpdateCamper(Camper camper)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Camper oldCamperData = camp.FetchCamper(camper.CamperCode);

            int requestId = request.AddNewCamperUpdateRequest(userId, camper, oldCamperData);
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests(requestId);

            return Ok();

        }

        [HttpPut("family")]
        public ActionResult UpdateFamily(Family family)
        {
            int userId = int.Parse(this.User.FindFirst("sub").Value);
            Family oldFamilyData = camp.FetchFamily(family.FamilyId);

            int requestId = request.AddNewFamilyUpdateRequest(userId, family, oldFamilyData);
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests(requestId);

            return Ok();

        }

        [HttpPut("approval/{requestId}")]
        public ActionResult UpdateRequestApproved(int requestId)
        {
            if (this.User.IsInRole("admin")) updates.ProcessApprovedRequests(requestId);
            return Ok();
        }
    }
}
