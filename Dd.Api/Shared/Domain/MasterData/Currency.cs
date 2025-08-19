using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class Currency : AuditableEntity, IHasName {
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}