using Carter;
using Dd.Api.Features.Injuries.Create;
using Dd.Api.Features.Injuries.Delete;
using Dd.Api.Features.Injuries.Get;
using Dd.Api.Features.Injuries.Update;
using Dd.Api.Shared.Application.GlobalResults;
using MediatR;

namespace Dd.Api.Features.Injuries;

public class InjuryModule : CarterModule {
    public override void AddRoutes(IEndpointRouteBuilder app) {
        var group = app.MapGroup("/injuries");
        
        group.MapPost("", async (CreateInjuryCommand command, ISender sender) => {
            var result = await sender.Send(command);
            return result.ToHttpResult();
        });

        group.MapGet("", async (ISender sender) => {
            var results = await sender.Send(new GetAllInjuriesQuery());
            return results.ToHttpResult();
        });
        
        group.MapGet("/{id:guid}", async (Guid id, ISender sender) => {
            var result = await sender.Send(new GetInjuryQuery{Id = id});
            return result.ToHttpResult();
        });
        
        group.MapPut("/{id:guid}", 
            async (Guid id, UpdateInjuryCommand command, ISender sender) => {
            
                command.Id = id;
                var result = await sender.Send(command);
                return result.ToHttpResult();
            });
        
        group.MapDelete("/{id:guid}", async (Guid id, ISender sender) => {
            var result = await sender.Send(new DeleteInjuryCommand { Id = id });
            return result.ToHttpResult();
        });
    }
}