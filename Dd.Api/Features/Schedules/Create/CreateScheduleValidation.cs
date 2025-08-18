using Dd.Api.Features.Schedules.Domain.Enums;
using FluentValidation;

namespace Dd.Api.Features.Schedules.Create;

public class CreateScheduleValidation : AbstractValidator<CreateScheduleCommand> {
    public CreateScheduleValidation() {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("End date must be after start date.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be after start time.");

        RuleFor(x => x.RecurrenceType)
            .IsInEnum().WithMessage("Invalid recurrence type.");

        RuleFor(x => x.DaysOfWeek)
            .NotEmpty().When(x => x.RecurrenceType == RecurrenceType.Weekly)
            .WithMessage("At least one day of week must be specified for weekly recurrence.");
        
        RuleFor(x => x.RecurrenceInterval)
            .GreaterThan(0).WithMessage("RecurrenceInterval must be greater than 0.");
    }
}