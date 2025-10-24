using System;
using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_Claim_System.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; }
        public string BlobUri { get; set; } // local path 
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    }
}
