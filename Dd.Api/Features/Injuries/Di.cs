using Dd.Api.Features.Injuries.Repos;

namespace Dd.Api.Features.Injuries;

public static class Di {
    public static void AddInjuryServices(this IServiceCollection services, IConfiguration config) {
        services.AddScoped<IInjuryRepo, InjuryRepo>();
    }
}