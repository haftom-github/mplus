namespace Dd.Api.Shared.Domain.Entities;

public interface IAuditable {
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
    // public DateTime? DeletedAt { get; set; }
    // public string CreatedBy { get; init; }
    // public string UpdatedBy { get; set; }
    // public string? DeletedBy { get; set; }
}