namespace Restaurant.Actors
{
    using System;
    using System.Threading;

    public class Cook : IOrderHandler
    {
        private static readonly Random Random = new Random();
        private readonly string name;
        private readonly int cookTime;

        public Cook(string name)
        {
            cookTime = Random.Next(500);
            this.name = name;
        }

        public void Handle(Order order)
        {
            
            Thread.Sleep(cookTime);
            order.CookTime = cookTime;
            order.Cook = name;
            Dispatcher.Instance.Publish("assistant", order);
        }


        public string Name
        {
            get { return name; }
        }
    }
}
