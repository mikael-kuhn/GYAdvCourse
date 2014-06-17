using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintOrderHandler printOrderHandler = new PrintOrderHandler();
            Cashier cashier = new Cashier(printOrderHandler);
            AssistingManager assistingManager = new AssistingManager(cashier, "Diana");
            Cook cook = new Cook(assistingManager, "John");
            Waiter waiter = new Waiter(cook, "Georgie");
            waiter.PlaceOrder(new List<int> { 1, 2 });
            cashier.Pay(cashier.GetOutstandingOrders().First(), "123456");
            Console.ReadLine();
        }
    }
}
