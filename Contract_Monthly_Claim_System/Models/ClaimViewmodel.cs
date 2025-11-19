using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_Claim_System.Models
{
    public class ClaimViewmodel
    {
        [Required]
        [Range(1, 300)]
        public int HoursWorked { get; set; }

        [Required]
        [Range(50, 1000)]
        public decimal HourlyRate { get; set; }

        public string Notes { get; set; }
    }
}
