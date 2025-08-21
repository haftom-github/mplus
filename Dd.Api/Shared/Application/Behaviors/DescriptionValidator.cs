using Dd.Api.Shared.Domain.Entities;
using FluentValidation;

namespace Dd.Api.Shared.Application.Behaviors;

public class DescriptionValidator : AbstractValidator<IHasDescription> {
    public DescriptionValidator() {
        RuleFor(x => x.Description)
            .MaximumLength(250).WithMessage("Description cannot exceed 500 characters");
    }
}