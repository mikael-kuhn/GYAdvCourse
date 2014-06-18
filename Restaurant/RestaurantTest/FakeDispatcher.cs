using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTest
{
    using Restaurant;
    using Restaurant.Events;

    public sealed class FakeDispatcher : IDispatcher
    {
        public void Subscribe<T>(Type type, IEventHandler<T> handler) where T : Restaurant.Events.IEvent
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>(Guid correlationId, IEventHandler<T> handler) where T : Restaurant.Events.IEvent
        {
            throw new NotImplementedException();
        }

        public void Publish(Restaurant.Events.IEvent @event)
        {
            Published.Add(@event);
        }

        public List<IEvent> Published = new List<IEvent>();

    }
}
