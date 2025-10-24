﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace Contract_Monthly_Claim_System.Models
{
    public enum ClaimStatus { Draft, Pending, Verified, Approved, Rejected, Settled }

    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required, MaxLength(200)]
        public string LecturerName { get; set; }

        [Required]
        public DateTime ClaimPeriod { get; set; } // Use month/year


        [Required]
        [Range(0, 1000)]
        public decimal HoursWorked { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; }

        [NotMapped]
        public decimal Amount => HoursWorked * HourlyRate;

        public string Notes { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Draft;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
