namespace Restaurant
{
    using Restaurant.Events;

    public interface IEventHandler<in T> where T: IEvent
    {
        void Handle(T @event);

        string Name { get; }
    }
}
