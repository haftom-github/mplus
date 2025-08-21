using Dd.Api.Shared.Application.Cqrs;

namespace Dd.Api.Features.Injuries.Delete;

public class DeleteInjuryCommand : ICommand {
    public Guid Id { get; set; }
}