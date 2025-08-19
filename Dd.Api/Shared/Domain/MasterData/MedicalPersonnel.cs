using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Domain.Entities;

namespace Dd.Api.Shared.Domain.MasterData;

public class MedicalPersonnel : AuditableEntity {

    public List<Specialization> Specializations { get; set; } = [];
    public Guid ActiveSpecializationId { get; set; }
    public Specialization? ActiveSpecialization { get; set; }
    
    public List<WorkSchedule> WorkSchedules { get; set; } = [];
    public List<BlockedSchedule> BlockedSchedules { get; set; } = [];
    
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    
    public int MaxAppointmentsPerDay { get; set; }
}