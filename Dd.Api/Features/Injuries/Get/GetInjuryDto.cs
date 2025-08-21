using Dd.Api.Shared.Domain.MasterData;

namespace Dd.Api.Features.Injuries.Get;

public record GetInjuryDto(Guid Id, string Name, string Description);

public static class InjuryExtension {
    public static GetInjuryDto ToGetInjuryDto(this Injury injury) {
        return new GetInjuryDto (
            injury.Id,
            injury.Name,
            injury.Description
        );
    }
}