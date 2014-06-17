using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    using System.Threading;

    using Restaurant.Actors;

    class Program
    {
        static void Main(string[] args)
        {
            PrintOrderHandler printOrderHandler = new PrintOrderHandler();
            Cashier cashier = new Cashier(printOrderHandler);
            QueuedHandler cashierProxy = new QueuedHandler(cashier);
            QueuedHandler assistingManager = new QueuedHandler(new AssistingManager("Diana"));
            QueuedHandler cook1 = new QueuedHandler(new TimeToLiveHandler(new Cook("John")));
            QueuedHandler cook2 = new QueuedHandler(new TimeToLiveHandler(new Cook("Peter")));
            QueuedHandler cook3 = new QueuedHandler(new TimeToLiveHandler(new Cook("Gregory")));

            var betterHandler = new QueuedHandler(new TimeToLiveHandler(new BetterDispatcher(new[] { cook1, cook2, cook3 })));

            Waiter waiter = new Waiter("Georgie");

            Dispatcher.Instance.Subscribe("cook", betterHandler);
            Dispatcher.Instance.Subscribe("assistant", assistingManager);
            Dispatcher.Instance.Subscribe("takepayment", cashierProxy);

            List<QueuedHandler> allHandlers = new List<QueuedHandler> { betterHandler, cashierProxy, assistingManager, cook1, cook2, cook3 };
            Monitor monitor = new Monitor(allHandlers);
            monitor.Start();

            foreach (IStartable handler in allHandlers)
            {
                handler.Start();
            }

            for (int i = 0; i < 1000; i++)
            {
                waiter.PlaceOrder(new List<int> { 1, 2 });
                Thread.Sleep(10);
            }
            while (true)
            {
                foreach (string orderId in cashier.GetOutstandingOrders())
                {
                    Console.WriteLine("Got outstanding order to pay, id " + orderId);
                    cashier.Pay(orderId, "123456");
                }
                Thread.Sleep(1);
            }
            //Console.ReadLine();
        }
    }
}
