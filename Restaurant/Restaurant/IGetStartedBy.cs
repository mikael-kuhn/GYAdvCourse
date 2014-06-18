namespace Restaurant
{
    using Restaurant.Events;

    public interface IGetStartedBy<T> where T: IEvent
    {
         void Start(T @event);
    }
}