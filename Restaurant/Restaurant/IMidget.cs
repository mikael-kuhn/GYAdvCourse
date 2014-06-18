namespace Restaurant
{
    using System;

    using Restaurant.Events;

    public interface IMidget
    {
        event EventHandler<EventArgs> OnCompleted;

        Guid CorrelationId { get; }
    }
}