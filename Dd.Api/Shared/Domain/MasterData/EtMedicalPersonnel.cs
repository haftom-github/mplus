using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class EtMedicalPersonnel : MedicalPersonnel {
    public Guid EmployeeId { get; set; }
}