using FluentValidation;

namespace Dd.Api.Features.Injuries.Get;

public class GetInjuryValidator : AbstractValidator<GetInjuryQuery>{

    public GetInjuryValidator() {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}