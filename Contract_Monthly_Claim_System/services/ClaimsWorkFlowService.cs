using Contract_Monthly_Claim_System.Models;
using Contract_Monthly_Claim_System.services.Validation;
using Contract_Monthly_Claim_System.Data;
using FluentValidation.Results;

namespace Contract_Monthly_Claim_System.services
{
    public class ClaimsWorkFlowService
    {
        private readonly ApplicationDbContext _context;
        private readonly ClaimRulesValidator _validator;

        public ClaimsWorkFlowService(ApplicationDbContext context)
        {
            _context = context;
            _validator = new ClaimRulesValidator();
        }

        public ValidationResult ValidateClaim(Claim claim)
        {
            return _validator.Validate(claim);
        }

        public async Task<bool> ApproveClaimAsync(int claimId, string approvedBy, ClaimStatus Approved)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim == null) return false;

            claim.Status = Approved;
            claim.ApprovedBy = approvedBy;
            claim.ApprovedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> RejectClaimAsync(int claimId, string approvedBy, string reason, ClaimStatus Rejected)
        {
            var claim = await _context.Claims.FindAsync(claimId);
            if (claim == null) return false;

            claim.Status = Rejected;
            claim.ApprovedBy = approvedBy;
            claim.RejectionReason = reason;
            claim.ApprovedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
