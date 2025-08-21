using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Application.Repositories;
using Dd.Api.Shared.Infrastructure.Persistence;

namespace Dd.Api.Features.Schedules.Infrastructure;

public class ScheduleRepo(AppDbContext context) : GenericRepo<Schedule>(context), IScheduleRepo;