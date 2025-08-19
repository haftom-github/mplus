using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public abstract class Service : AuditableEntity, IHasName, IHasDescription {
    
    public Guid CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public double Price { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly EffectiveTo { get; set; }
    
    public double LastPrice { get; set; }
    public DateOnly LastPriceEffectiveFrom { get; set; }
    public DateOnly LastPriceEffectiveTo { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}