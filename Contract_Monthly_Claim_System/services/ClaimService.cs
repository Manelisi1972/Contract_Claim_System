using System;
using System.IO;
using System.Threading.Tasks;
using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.Models;
using Contract_Monthly_Claim_System.services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Contract_Monthly_Claim_System.services
{
    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        public async Task<Attachment> AddAttachmentAsync(int claimId, IFormFile file, string uploadedBy)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file upload.");

            // Allowed file types
            var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!Array.Exists(allowedExtensions, e => e == extension))
                throw new InvalidOperationException("Unsupported file type. Allowed: PDF, DOCX, XLSX");

            if (file.Length > 5 * 1024 * 1024)
                throw new InvalidOperationException("File too large. Max size is 5MB.");

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                ClaimId = claimId,
                FileName = file.FileName,
                FileType = extension.Trim('.'),
                FileSize = file.Length,
                FilePath = $"/uploads/{fileName}",
                UploadedBy = uploadedBy,
                UploadedAt = DateTime.Now
            };

            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            return attachment;
        }

        public async Task<Claim> AddClaimAsync(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public Task ApproveAsync(int id, string v)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetAllForLecturerAsync(string lecturer)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Attachment>> GetAttachmentsByClaimIdAsync(int claimId)
        {
            return await _context.Attachments.Where(a => a.ClaimId == claimId).ToListAsync();
        }

        public Task<string?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetPendingAsync()
        {
            throw new NotImplementedException();
        }

        public Task RejectAsync(int id, string v, string reason)
        {
            throw new NotImplementedException();
        }

        
    }
}
