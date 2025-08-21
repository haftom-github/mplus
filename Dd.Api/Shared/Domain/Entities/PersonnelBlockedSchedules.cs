using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Domain.MasterData;

namespace Dd.Api.Shared.Domain.Entities;

public class PersonnelBlockedSchedules : AuditableEntity {
    public Guid MedicalPersonnelId { get; set; }
    public MedicalPersonnel? MedicalPersonnel { get; set; }
    public Guid BlockedScheduleId { get; set; }
    public BlockedSchedule? BlockedSchedule { get; set; }
}