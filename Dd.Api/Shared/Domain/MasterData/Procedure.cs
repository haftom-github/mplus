namespace Dd.Api.Shared.Domain.MasterData;

public class Procedure : Service {
    
    public Guid SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
}