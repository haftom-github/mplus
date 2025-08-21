using Dd.Api.Shared.Application.Behaviors;
using FluentValidation;

namespace Dd.Api.Features.Injuries.Update;

public class UpdateInjuryValidator : AbstractValidator<UpdateInjuryCommand> {
    public UpdateInjuryValidator() {
        Include(new NameValidator());
        Include(new DescriptionValidator());
    }
}