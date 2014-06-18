namespace Restaurant
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Restaurant.Events;

    public class MidgetHouse : IGetStartedBy<OrderPlaced>, IEventHandler<IEvent>, IMidgetHouse
    {
        private readonly Dictionary<Guid, IMidget> midgets = new Dictionary<Guid, IMidget>();

        private readonly IDispatcher dispatcher;

        public MidgetHouse(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public void Start(OrderPlaced @event)
        {
            var id = new Guid(@event.Order.Id);
            dynamic midget = new OrderMidget(id, dispatcher);
            ((IMidget)midget).OnCompleted += MidgetCompleted;
            midgets.Add(id, midget);
        }

        private void MidgetCompleted(object sender, EventArgs e)
        {
            midgets.Remove(((IMidget)sender).CorrelationId);
        }

        public void Handle(IEvent @event)
        {
            //Console.WriteLine(
            //    "MidgetHouse handling event {0} order id {1}",
            //    @event.GetType().Name,
            //    @event.CorrelationId);

            if (@event is OrderPlaced)
            {
                Start((OrderPlaced)@event);
            }

            if (!midgets.ContainsKey(@event.CorrelationId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No midget found for correlation id " + @event.CorrelationId);
                Console.ResetColor();
                return;
            }
            object midget = midgets[@event.CorrelationId];
            MethodInfo method = midget.GetType().GetMethod("Handle", new[] { @event.GetType() });
            method.Invoke(midget, new object[] { @event });
        }

        public string Name { get; private set; }

        public int MidgetsCount {
            get
            {
                return midgets.Count;
            }
        }
    }

    public interface IMidgetHouse
    {
        int MidgetsCount { get; }
    }
}