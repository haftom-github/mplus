using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class DiseaseCategory : AuditableEntity, IHasName, IHasDescription {
    
    // name and description
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}