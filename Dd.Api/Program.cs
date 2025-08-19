using Dd.Api.Features.Schedules.Create;
using Dd.Api.Features.Schedules.Get;
using Dd.Api.Shared.Application.Middleware;
using Dd.Api.Shared.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAllFeaturesServices(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.MapGet("/schedules", async (ISender sender) => {
    var result = await sender.Send(new GetSchedulesQuery());
    return result;
});

app.MapPost("/schedules", async (CreateScheduleCommand command, ISender sender) => {
    var result = await sender.Send(command);
    return result;
});

app.UseHttpsRedirection();
app.Run();