namespace Dd.Api.Shared.Domain.Entities;

public abstract class AuditableEntity : Entity, IAuditable {
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
    
    public string CreatedBy { get; init; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;

    public RecordStatus Status { get; set; }
    public DateTime? StatusChangedAt { get; set; }
    public string? DeletedBy { get; set; }
}