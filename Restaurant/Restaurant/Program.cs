using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    using System.Collections;
    using System.Threading;

    using Restaurant.Actors;
    using Restaurant.Commands;
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
            var house = new MidgetHouse(Dispatcher.Instance);
            var houseQueue = new QueuedHandler(house);

            var clock = new AlarmClock();

            Dispatcher.Instance.Subscribe(typeof(OrderPlaced), houseQueue);
            Dispatcher.Instance.Subscribe(typeof(FoodCooked), houseQueue);
            Dispatcher.Instance.Subscribe(typeof(OrderPriced), houseQueue);
            Dispatcher.Instance.Subscribe(typeof(PaymentTaken), houseQueue);
            Dispatcher.Instance.Subscribe(typeof(FirstCookRetry), houseQueue);
            Dispatcher.Instance.Subscribe(typeof(SecondCookRetry), houseQueue);

            Dispatcher.Instance.Subscribe(typeof(CookFood), betterHandler);
            Dispatcher.Instance.Subscribe(typeof(PriceOrder), assistingManager);
            Dispatcher.Instance.Subscribe(typeof(TakePayment), cashierProxy);
            Dispatcher.Instance.Subscribe(typeof(PrintOrder), printOrderHandler);

            Dispatcher.Instance.Subscribe(typeof(SendToMeIn), clock);

            IEnumerable<QueuedHandler> allHandlers = new List<QueuedHandler> { houseQueue, betterHandler, cashierProxy, assistingManager, cook1, cook2, cook3, printOrderHandler };
            Monitor monitor = new Monitor(allHandlers, new [] {house});
            monitor.Start();

            foreach (IStartable handler in allHandlers)
            {
                handler.Start();
            }
            clock.Start();
            //waiter.PlaceOrder(new List<int> { 1, 2 });

            for (int i = 0; i < 1000; i++)
            {
                waiter.PlaceOrder(new List<int> { 1, 2 });
                Thread.Sleep(50);
            }
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
