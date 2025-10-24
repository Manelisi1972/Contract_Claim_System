﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract_Monthly_Claim_System.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Required]
        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public Claim Claim { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        [StringLength(50)]
        public string FileType { get; set; }

        public long FileSize { get; set; }

        [Required]
        public string FilePath { get; set; } // Path to saved file

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string UploadedBy { get; set; }

    }
}
