namespace Dd.Api.Shared.Domain.Entities;

public abstract class AuditableEntity : Entity, IAuditable {
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
    // public DateTime? DeletedAt { get; set; }
    // public string CreatedBy { get; init; } = string.Empty;
    // public string UpdatedBy { get; set; } = string.Empty;
    // public string? DeletedBy { get; set; }
}