using Dd.Api.Shared.Application.Repositories;
using Dd.Api.Shared.Domain.MasterData;
using Dd.Api.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Features.Injuries.Repos;

public class InjuryRepo(AppDbContext dbContext) 
    : GenericRepo<Injury>(dbContext), IInjuryRepo;