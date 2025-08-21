using Dd.Api.Shared.Application.Cqrs;
using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Features.Injuries.Update;

public class UpdateInjuryCommand : ICommand, IHasName, IHasDescription {
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}