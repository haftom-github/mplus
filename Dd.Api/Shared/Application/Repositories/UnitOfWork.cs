using Dd.Api.Features.Reservations.Contracts;
using Dd.Api.Features.Schedules.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Shared.Application.Repositories;

public class UnitOfWork(IAppointmentRepo appointmentRepo, 
    ISlotRepo slotRepo, 
    IScheduleRepo scheduleRepo,
    DbContext context) 
    : IUnitOfWork {

    public IAppointmentRepo AppointmentRepo { get; } = appointmentRepo;
    public ISlotRepo SlotRepo { get; } = slotRepo;
    public IScheduleRepo ScheduleRepo { get; } = scheduleRepo;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default) {
        return await context.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose() {
        context.Dispose();
    }
}