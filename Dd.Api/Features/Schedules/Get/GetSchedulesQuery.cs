using Dd.Api.Features.Schedules.Domain.Entities;
using MediatR;

namespace Dd.Api.Features.Schedules.Get;

public class GetSchedulesQuery : IRequest<IEnumerable<Schedule>> {
    
}