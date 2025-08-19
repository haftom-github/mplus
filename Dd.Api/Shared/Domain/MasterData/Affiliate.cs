using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class Affiliate : AuditableEntity, IHasName {
    public Guid AffiliateTypeId { get; set; }
    public AffiliateType? AffiliateType { get; set; }
    public string Name { get; set; } = string.Empty;
}