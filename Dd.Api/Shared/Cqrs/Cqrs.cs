using Dd.Api.Shared.Results;
using MediatR;

namespace Dd.Api.Shared.Cqrs;

// defines query with no response and its handler
public interface IQuery : IRequest<Result>;

public interface IQueryHandler<in TQuery> 
    : IRequestHandler<TQuery, Result>
    where TQuery : IQuery;




// defines query with response and its handler
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    where TResponse : notnull;

public interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull;




// defines command with no response and its handler
public interface ICommand : IRequest<Result>;

public interface ICommandHandler<in TCommand> 
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;




// defines command with response and its handler
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    where TResponse : notnull;

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull;