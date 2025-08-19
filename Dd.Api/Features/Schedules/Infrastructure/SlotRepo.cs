using Dd.Api.Features.Schedules.Contracts;
using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Features.Schedules.Infrastructure;

public class SlotRepo(DbContext context) : GenericRepo<TimeSlot>(context), ISlotRepo;