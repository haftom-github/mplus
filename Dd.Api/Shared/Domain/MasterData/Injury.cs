using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class Injury(string name, string description) : AuditableEntity, IHasName, IHasDescription {
    
    // name and description
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}