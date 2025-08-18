using Dd.Api.Features.Schedules.Contracts;

namespace Dd.Api.Features.Schedules.Infrastructure;

public static class Di {
    public static IServiceCollection AddSchedulesServices(this IServiceCollection services, IConfiguration config) {
        services.AddScoped<IScheduleRepo, InMemorySchedulesRepo>();
        return services;
    }
}