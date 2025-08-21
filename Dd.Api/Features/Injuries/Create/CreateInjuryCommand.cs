using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Features.Injuries.Create;

public class CreateInjuryCommand : ICommand<Guid>, IHasName, IHasDescription {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}