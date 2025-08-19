using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Cqrs;
using Dd.Api.Shared.Results;
using MediatR;

namespace Dd.Api.Features.Schedules.Get;

public class GetSchedulesQuery : IQuery<IEnumerable<Schedule>> {
    
}