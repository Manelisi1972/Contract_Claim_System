using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Contract_Monthly_Claim_System.Models;
using Microsoft.AspNetCore.Hosting;
using Contract_Monthly_Claim_System.services.interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;

        public ClaimsController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        // GET: /Claims
        public async Task<IActionResult> Index(string lecturer = null)
        {
            // For prototype, allow lecturer name as query param or fallback
            lecturer ??= " Lecturer";
            var claims = await _claimService.GetAllForLecturerAsync(lecturer);
            ViewData["LecturerName"] = lecturer;
            return View(claims);
        }

        // GET /Claims/Details/
        public async Task<IActionResult> Details(int id)
        {
            var c = await _claimService.GetByIdAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }
        [HttpPost]
        public async Task<IActionResult> UploadAttachment(int claimId, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    TempData["Error"] = "Please select a file to upload.";
                    return RedirectToAction("Details", new { id = claimId });
                }

                var uploadedBy = User.Identity?.Name ?? "Unknown User";
                await _claimService.AddAttachmentAsync(claimId, file, uploadedBy);

                TempData["Success"] = "Attachment uploaded successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Upload failed: {ex.Message}";
            }

            return RedirectToAction("Details", new { id = claimId });
        }

        
    }
}
