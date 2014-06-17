using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    using System.Collections;
    using System.Threading;

    using Restaurant.Actors;
    using Restaurant.Events;
    using Restaurant.Handlers;

    class Program
    {
        static void Main(string[] args)
        {
            QueuedHandler printOrderHandler = new QueuedHandler(new PrintOrderHandler());
            IEventHandler<IEvent> cashier = new Cashier();
            QueuedHandler cashierProxy = new QueuedHandler(cashier);
            var assistingManager = new QueuedHandler(new AssistingManager("Diana"));
            var cook1 = new QueuedHandler(new TimeToLiveHandler(new Cook("John")));
            var cook2 = new QueuedHandler(new TimeToLiveHandler(new Cook("Peter")));
            var cook3 = new QueuedHandler(new TimeToLiveHandler(new Cook("Gregory")));

            var betterHandler = new QueuedHandler(new TimeToLiveHandler(new BetterDispatcher(new[] { cook1, cook2, cook3 })));

            Waiter waiter = new Waiter("Georgie");

            Dispatcher.Instance.Subscribe(typeof(OrderPlaced), printOrderHandler);
            Dispatcher.Instance.Subscribe(typeof(OrderPlaced), betterHandler);
            Dispatcher.Instance.Subscribe(typeof(FoodCooked), assistingManager);
            Dispatcher.Instance.Subscribe(typeof(OrderPriced), cashierProxy);

            IEnumerable<QueuedHandler> allHandlers = new List<QueuedHandler> { betterHandler, cashierProxy, assistingManager, cook1, cook2, cook3, printOrderHandler };
            Monitor monitor = new Monitor(allHandlers);
            monitor.Start();

            foreach (IStartable handler in allHandlers)
            {
                handler.Start();
            }


            waiter.PlaceOrder(new List<int> { 1, 2 });

            //for (int i = 0; i < 1000; i++)
            //{
            //    waiter.PlaceOrder(new List<int> { 1, 2 });
            //    Thread.Sleep(10);
            //}
            while (true)
            {
                foreach (string orderId in ((Cashier)cashier).GetOutstandingOrders())
                {
                    Console.WriteLine("Got outstanding order to pay, id " + orderId);
                    ((Cashier)cashier).Pay(orderId, "123456");
                }
                Thread.Sleep(1);
            }
            //Console.ReadLine();
        }
    }
}
