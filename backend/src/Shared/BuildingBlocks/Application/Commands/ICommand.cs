using MediatR;

namespace BuildingBlocks.Application.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
