namespace Dd.Api.Shared.Domain.MasterData;

public class MedicalPersonnelSpecializations {
    public Guid MedicalPersonnelId { get; set; }
    public MedicalPersonnel? MedicalPersonnel { get; set; }
    public Guid SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
    public bool IsActive { get; set; } = true;
}