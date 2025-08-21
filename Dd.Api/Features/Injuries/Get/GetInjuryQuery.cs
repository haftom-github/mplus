using Dd.Api.Shared.Application.Cqrs;

namespace Dd.Api.Features.Injuries.Get;

public class GetInjuryQuery : IQuery<GetInjuryDto> {
    public Guid Id { get; set; }
}