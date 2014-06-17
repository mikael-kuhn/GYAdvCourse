using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            PrintOrderHandler printOrderHandler = new PrintOrderHandler();
            Cashier cashier = new Cashier(printOrderHandler);
            QueuedHandler cashierProxy = new QueuedHandler(cashier);
            QueuedHandler assistingManager = new QueuedHandler(new AssistingManager(cashierProxy, "Diana"));
            QueuedHandler cook1 = new QueuedHandler(new Cook(assistingManager, "John"));
            QueuedHandler cook2 = new QueuedHandler(new Cook(assistingManager, "Peter"));
            QueuedHandler cook3 = new QueuedHandler(new Cook(assistingManager, "Gregory"));

            RoundRobinHandler roundRobinHandler = new RoundRobinHandler(new[] { cook1, cook2, cook3 });

            Waiter waiter = new Waiter(roundRobinHandler, "Georgie");

            List<IStartable> allHandlers = new List<IStartable> { cashierProxy, assistingManager, cook1, cook2, cook3 };
            foreach (IStartable handler in allHandlers)
            {
                handler.Start();
            }

            waiter.PlaceOrder(new List<int> { 1, 2 });
            waiter.PlaceOrder(new List<int> { 1, 2 });
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
