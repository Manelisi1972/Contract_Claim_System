using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.Models;

namespace Contract_Monthly_Claim_System.services
{
    public class InvoiceService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Claim> GetApprovedClaims(DateTime start, DateTime end)
    {
        return _context.Claims
            .Where(c => c.Status == ClaimStatus.Approved &&
                        c.ApprovedDate >= start &&
                        c.ApprovedDate <= end)
            .ToList();
    }
    }
}
