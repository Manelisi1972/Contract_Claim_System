using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Contract_Monthly_Claim_System.Models;

namespace Contract_Monthly_Claim_System.services.interfaces
{
    public interface IClaimService
    {
        Task<Claim> AddClaimAsync(Claim claim);
        Task<Attachment> AddAttachmentAsync(int claimId, IFormFile file, string uploadedBy);
        Task<IEnumerable<Attachment>> GetAttachmentsByClaimIdAsync(int claimId);
        Task<string?> GetPendingAsync();
        Task ApproveAsync(int id, string v);
        Task RejectAsync(int id, string v, string reason);
        Task<string?> GetByIdAsync(int id);
        Task<string?> GetAllForLecturerAsync(string lecturer);
    }
}
