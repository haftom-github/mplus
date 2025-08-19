using Dd.Api.Features.Schedules.Domain.Entities;
using Dd.Api.Shared.Application.Cqrs;
using MediatR;

namespace Dd.Api.Features.Schedules.Get;

public class GetSchedulesQuery : IQuery<IEnumerable<Schedule>> {
    
}