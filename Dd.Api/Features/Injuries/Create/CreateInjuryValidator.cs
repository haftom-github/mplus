using Dd.Api.Shared.Application.Behaviors;
using FluentValidation;

namespace Dd.Api.Features.Injuries.Create;

public class CreateInjuryValidator : AbstractValidator<CreateInjuryCommand> {
    public CreateInjuryValidator() {
        Include(new NameValidator());
        Include(new DescriptionValidator());
    }
}