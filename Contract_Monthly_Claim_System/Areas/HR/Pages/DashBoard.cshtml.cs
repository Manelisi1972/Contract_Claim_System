using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Contract_Monthly_Claim_System.Areas.HR.Pages
{
    [Authorize(Roles = "HR,Manager")]
    public class DashBoardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashBoardModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public int ApprovedClaimsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public int LecturerCount { get; set; }
        public void OnGet()
        {
            ApprovedClaimsCount = _context.Claims.Count(c => c.Status == ClaimStatus.Approved);
            TotalAmount = _context.Claims.Where(c => c.Status == ClaimStatus.Approved)
                                         .Sum(c => c.TotalAmount);

          LecturerCount = _context.Lecterers;
        }
    }
}
