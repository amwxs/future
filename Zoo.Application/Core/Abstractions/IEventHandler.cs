using MediatR;

namespace Zoo.Application.Core.Abstractions;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : INotification
{
}
