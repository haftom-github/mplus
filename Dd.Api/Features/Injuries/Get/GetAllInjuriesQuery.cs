using Dd.Api.Shared.Application.Cqrs;

namespace Dd.Api.Features.Injuries.Get;

public class GetAllInjuriesQuery : IQuery<IEnumerable<GetInjuryDto>>;