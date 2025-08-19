using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class DiseaseSubCategory : AuditableEntity, IHasName, IHasDescription {
    
    public Guid DiseaseCategoryId { get; set; }
    public DiseaseCategory? DiseaseCategory { get; set; }
    
    // name and description
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}