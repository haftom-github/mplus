using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Domain.MasterData;

namespace Dd.Api.Shared.Domain.Entities;

public class PersonnelWorkSchedules : AuditableEntity {
    public Guid MedicalPersonnelId { get; set; }
    public MedicalPersonnel? MedicalPersonnel { get; set; }
    public Guid WorkScheduleId { get; set; }
    public WorkSchedule? WorkSchedule { get; set; }
}

