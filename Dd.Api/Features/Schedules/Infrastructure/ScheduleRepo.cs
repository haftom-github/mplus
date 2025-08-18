using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Features.Schedules.Infrastructure;

public class ScheduleRepo(DbContext context) : GenericRepo<Schedule>(context), IScheduleRepo;