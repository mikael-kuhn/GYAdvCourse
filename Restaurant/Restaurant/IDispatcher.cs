namespace Restaurant
{
    using System;

    using Restaurant.Events;

    public interface IDispatcher
    {
        void Subscribe<T>(Type type, IEventHandler<T> handler) where T : IEvent;

        void Subscribe<T>(Guid correlationId, IEventHandler<T> handler) where T : IEvent;
        void Publish(IEvent @event);
    }
}