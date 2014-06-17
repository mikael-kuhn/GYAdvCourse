using System;

namespace Restaurant.Events
{
    public interface IEvent
    {
        DateTime? TimeToLive { get; set; }

        Guid Id { get; set; }

        Guid CorrelationId { get; set; }

        Guid CausationId { get; set; }
    }
}
