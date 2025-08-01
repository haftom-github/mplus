namespace Dd.Domain.Common.Entities;

public abstract class Entity : Auditable {
    public Guid Id { get; set; } = Guid.NewGuid();
}