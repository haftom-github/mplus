using Carter;
using Dd.Api.Features.Injuries.Create;
using Dd.Api.Features.Injuries.Delete;
using Dd.Api.Features.Injuries.Get;
using Dd.Api.Features.Injuries.Update;
using MediatR;

namespace Dd.Api.Features.Injuries;

public class InjuryModule : CarterModule {
    public override void AddRoutes(IEndpointRouteBuilder app) {
        app.MapPost("/injuries", async (CreateInjuryCommand command, ISender sender) => {
            var result = await sender.Send(command);
            return result;
        });

        app.MapGet("/injuries", async (ISender sender) => {
            var results = await sender.Send(new GetAllInjuriesQuery());
            return results;
        });
        
        app.MapGet("/injuries/{id:guid}", async (Guid id, ISender sender) => {
            var result = await sender.Send(new GetInjuryQuery{Id = id});
            return result;
        });
        
        app.MapPut("/injuries/{id:guid}", 
            async (Guid id, UpdateInjuryCommand command, ISender sender) => {
            
                command.Id = id;
                var result = await sender.Send(command);
                return result;
            });
        
        app.MapDelete("/injuries/{id:guid}", async (Guid id, ISender sender) => {
            var result = await sender.Send(new DeleteInjuryCommand { Id = id });
            return result;
        });
    }
}