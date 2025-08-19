using Dd.Api.Features.Schedules.Infrastructure;
using Dd.Api.Shared.Application.Behaviors;
using Dd.Api.Shared.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Shared.Infrastructure;

public static class Di {
    public static IServiceCollection AddAllFeaturesServices(this IServiceCollection services, IConfiguration config) {
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(config.GetConnectionString("DefaultConnection")));
        services.ConfigureHttpJsonOptions(options => {
            options.SerializerOptions.Converters.Add(
                new System.Text.Json.Serialization.JsonStringEnumConverter(
                    System.Text.Json.JsonNamingPolicy.CamelCase,
                    allowIntegerValues: true
                )
            );
        });
        services.AddValidatorsFromAssemblyContaining<Program>();
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        });
        services.AddSchedulesServices(config);
        return services;
    }
}