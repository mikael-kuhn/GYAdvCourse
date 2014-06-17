using System;

namespace Restaurant.Events
{
    public interface IEvent
    {
        DateTime? TimeToLive { get; set; }
    }
}
