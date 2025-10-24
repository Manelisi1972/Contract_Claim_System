using System.Threading.Tasks;
using Contract_Monthly_Claim_System.services;
using Contract_Monthly_Claim_System.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly IClaimService _claimService;
        public AdminController(IClaimService claimService) => _claimService = claimService;

        public async Task<IActionResult> Index()
        {
            var pending = await _claimService.GetPendingAsync();
            return View(pending);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await _claimService.ApproveAsync(id, User.Identity?.Name ?? "Coordinator");
                TempData["Success"] = "Claim approved.";
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = "Unable to approve: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason)) reason = "No reason provided.";
            try
            {
                await _claimService.RejectAsync(id, User.Identity?.Name ?? "Coordinator", reason);
                TempData["Success"] = "Claim rejected.";
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = "Unable to reject: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
