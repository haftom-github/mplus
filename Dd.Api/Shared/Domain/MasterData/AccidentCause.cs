using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class AccidentCause : AuditableEntity, IHasName, IHasDescription {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}