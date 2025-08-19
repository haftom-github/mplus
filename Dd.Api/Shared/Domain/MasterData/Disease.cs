using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class Disease : AuditableEntity, IHasName, IHasDescription {
    
    public string DiseaseCode { get; set; } = string.Empty;
    public Guid SubCategoryId { get; set; }
    public DiseaseSubCategory? SubCategory { get; set; }
    
    
    // name and description
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}