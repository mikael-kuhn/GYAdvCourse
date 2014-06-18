namespace RestaurantTest
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Restaurant.Actors;
    using Restaurant.Commands;
    using Restaurant.Events;
    using Restaurant;

    [TestClass]
    public class OrderMidgetTest
    {
        private OrderMidget testSubject;
        private FakeDispatcher dispatcher;
        private Order order;

        [TestInitialize]
        public void TestInitialize()
        {
            order = new Order(string.Empty);
            dispatcher = new FakeDispatcher();
            testSubject = new OrderMidget(new Guid(order.Id), dispatcher);
        }

        [TestMethod]
        public void HandleOrderPlaced()
        {
            // Act
            testSubject.Handle(new OrderPlaced(order));
            // Assert
            Assert.AreEqual(2, dispatcher.Published.Count, "expected 1 event");
            Assert.AreEqual(typeof(CookFood), dispatcher.Published.First().GetType(), "expected a different type");
            Assert.AreEqual(typeof(SendToMeIn), dispatcher.Published.Last().GetType(), "expected a different type");
        }

        [TestMethod]
        public void HandleFoodCooked()
        {
            // Act
            testSubject.Handle(new FoodCooked(order, Guid.NewGuid()));
            // Assert
            Assert.AreEqual(1, dispatcher.Published.Count, "expected 1 event");
            Assert.AreEqual(typeof(PriceOrder), dispatcher.Published.First().GetType(), "expected a different type");
        }

        [TestMethod]
        public void HandleOrderPriced()
        {
            // Act
            testSubject.Handle(new OrderPriced(order, Guid.NewGuid()));
            // Assert
            Assert.AreEqual(1, dispatcher.Published.Count, "expected 1 event");
            Assert.AreEqual(typeof(TakePayment), dispatcher.Published.First().GetType(), "expected a different type");
        }

        [TestMethod]
        public void HandlePaymentTaken()
        {
            // Act
            testSubject.Handle(new PaymentTaken(order, Guid.NewGuid()));
            // Assert
            Assert.AreEqual(1, dispatcher.Published.Count, "expected 1 event");
            Assert.AreEqual(typeof(PrintOrder), dispatcher.Published.First().GetType(), "expected a different type");
        }

        [TestMethod]
        public void HandleFirstCookRetry()
        {
            // Act
            testSubject.Handle(new FirstCookRetry(order, Guid.NewGuid()));
            // Assert
            Assert.AreEqual(1, dispatcher.Published.Count, "expected 1 event");
            Assert.AreEqual(typeof(SecondCookRetry), dispatcher.Published.First().GetType(), "expected a different type");
        }

        [TestMethod]
        public void HandleFirstCookRetryWhenFoodHasBeenCooked()
        {
            // Arrange
            testSubject.Handle(new FoodCooked(order, Guid.NewGuid()));
            dispatcher.Published.Clear();

            // Act
            testSubject.Handle(new FirstCookRetry(order, Guid.NewGuid()));

            // Assert
            Assert.AreEqual(0, dispatcher.Published.Count, "expected 1 event");
        }

    }
}
