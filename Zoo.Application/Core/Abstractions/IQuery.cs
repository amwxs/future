using MediatR;

namespace Zoo.Application.Core.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
