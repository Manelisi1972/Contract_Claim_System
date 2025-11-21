using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contract_Monthly_Claim_System.Controllers
{
    [Route("api/claims")]
    [ApiController]
    public class ClaimsApiController : Controller
    {
        private readonly ClaimsWorkFlowService _workflow;
        private readonly ApplicationDbContext _context;

        public ClaimsApiController(ClaimsWorkFlowService workflow, ApplicationDbContext context)
        {
            _workflow = workflow;
            _context = context;
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var success = await _workflow.ApproveClaimAsync(id, User.Identity.Name);
            return success ? Ok(new { message = "Claim Approved" }) : BadRequest();
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectClaim(int id, [FromBody] string reason)
        {
            var success = await _workflow.RejectClaimAsync(id, User.Identity.Name, reason);
            return success ? Ok(new { message = "Claim Rejected" }) : BadRequest();
        }
    }
}
