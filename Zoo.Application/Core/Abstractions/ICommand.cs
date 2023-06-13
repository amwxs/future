using MediatR;

namespace Zoo.Application.Core.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
