using Contract_Monthly_Claim_System.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Controllers
{
    [Authorize(Roles = "Coordinator,Manager")]
    public class ApprovalController : Controller
    {
        private readonly ClaimsWorkFlowService _workflow;

        public ApprovalController(ClaimsWorkFlowService workflow)
        {
            _workflow = workflow;
        }
    }
}
