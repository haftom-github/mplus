using Dd.Api.Shared.Domain.Entities;
using FluentValidation;

namespace Dd.Api.Shared.Application.Behaviors;

public class NameValidator : AbstractValidator<IHasName> {
    public NameValidator() {
        RuleFor(x => x.Name)
            .MaximumLength(25).WithMessage("Name cannot exceed 100 characters");
    }
}