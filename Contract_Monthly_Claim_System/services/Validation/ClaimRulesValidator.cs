using FluentValidation;
using Contract_Monthly_Claim_System.Models;


namespace Contract_Monthly_Claim_System.services.Validation
{
    public class ClaimRulesValidator : AbstractValidator<Claim>
    {

        public ClaimRulesValidator() {

            RuleFor(c => c.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than zero.");

            RuleFor(c => c.ClaimPeriodStart)
                .LessThan(c => c.ClaimPeriodEnd)
                .WithMessage("Claim start date must be before end date.");

            RuleFor(c => c.HoursWorked)
                .InclusiveBetween(1, 300)
                .WithMessage("Hours worked must be between 1 and 300.");

            RuleFor(c => c.HourlyRate)
                .InclusiveBetween(50, 1000)
                .WithMessage("Hourly rate must be between R50 and R1000.");
        }
    }
}
