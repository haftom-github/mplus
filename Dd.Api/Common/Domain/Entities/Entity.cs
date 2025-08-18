namespace Dd.Api.Common.Domain.Entities;

public abstract class Entity : Auditable {
    public Guid Id { get; set; } = Guid.NewGuid();
}