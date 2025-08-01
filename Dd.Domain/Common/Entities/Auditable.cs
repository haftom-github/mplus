namespace Dd.Domain.Common.Entities;

public abstract class Auditable {
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}