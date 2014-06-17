using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LessionOneTest
{
    using LessonOne;

    [TestClass]
    public class TrailingStopLessTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }


        [TestMethod]
        public void PositionAquired()
        {
            // Arrange
            FakePublisher publisher = new FakePublisher();
            TrailingStopLess manager = new TrailingStopLess(publisher);

            // Act
            manager.Hande(
                new PositionAcquired(2.90, TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(17)));

            // Assert
            Assert.AreEqual(1, publisher.PublishedMessages.Count, "expected one message");
            ThresholdChanged message = publisher.PublishedMessages.Last() as ThresholdChanged;
            Assert.IsNotNull(message, "expected a different type");
            Assert.AreEqual(2.90, message.Threshold, "expected a different threshold");
        }

        [TestMethod]
        public void PriceUpdated()
        {
            // Arrange
            FakePublisher publisher = new FakePublisher();
            TrailingStopLess manager = new TrailingStopLess(publisher);
            manager.Hande(
                new PositionAcquired(2.90, TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(17)));
            publisher.PublishedMessages.Clear();

            // Act
            manager.Hande(new PriceUpdated(2.90));

            // Assert
            Assert.AreEqual(2, publisher.PublishedMessages.Count, "expected two message");
            Assert.AreEqual(
                typeof(SendToMeIn),
                publisher.PublishedMessages[0].GetType(),
                "expected a different type of message (1)");
            Assert.AreEqual(
                typeof(SendToMeIn),
                publisher.PublishedMessages[1].GetType(),
                "expected a different type of message (2)");

            SendToMeIn message = publisher.PublishedMessages[0] as SendToMeIn;
            Assert.AreEqual(typeof(RemoveMoveUpPoint), message.Message.GetType(), "expected RemoveTopPrice");
            Assert.AreEqual(TimeSpan.FromSeconds(11), message.Seconds, "expected the same timespan (1)");

            message = publisher.PublishedMessages[1] as SendToMeIn;
            Assert.AreEqual(typeof(RemoveSellingPoint), message.Message.GetType(), "expected RemoveTopPrice");
            Assert.AreEqual(TimeSpan.FromSeconds(17), message.Seconds, "expected the same timespan (2)");
        }

        [TestMethod]
        public void RemoveSellingPointPrice_WhenPriceGoesUpItShouldDoNothing()
        {
            // Arrange
            FakePublisher publisher = new FakePublisher();
            TrailingStopLess manager = new TrailingStopLess(publisher);
            manager.Hande(
                new PositionAcquired(2.90, TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(17)));
            manager.Hande(new PriceUpdated(2.90));
            manager.Hande(new PriceUpdated(3.0));
            manager.Hande(new PriceUpdated(3.05));

            publisher.PublishedMessages.Clear();

            // Act
            manager.Hande(new RemoveSellingPoint(2.90));

            // Assert
            Assert.AreEqual(0, publisher.PublishedMessages.Count, "expected no messages");
        }

        [TestMethod]
        public void RemoveSellingPointPrice_WhenPriceGoesDownMakeTheSale()
        {
            // Arrange
            FakePublisher publisher = new FakePublisher();
            TrailingStopLess manager = new TrailingStopLess(publisher);
            manager.Hande(
                new PositionAcquired(2.90, TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(17)));
            manager.Hande(new PriceUpdated(2.90));
            manager.Hande(new PriceUpdated(2.5));
            manager.Hande(new PriceUpdated(2.0));

            publisher.PublishedMessages.Clear();

            // Act
            manager.Hande(new RemoveSellingPoint(2.90));
            manager.Hande(new RemoveSellingPoint(2.50));

            // Assert
            Assert.AreEqual(1, publisher.PublishedMessages.Count, "expected no messages");
            Assert.AreEqual(typeof(Sell), publisher.PublishedMessages[0].GetType(), "expected Sell");
        }

        [TestMethod]
        public void RemoveMoveUpPointPrice_ShouldDoNothingWhenPriceIsGoingDown()
        {
            // Arrange
            FakePublisher publisher = new FakePublisher();
            TrailingStopLess manager = new TrailingStopLess(publisher);
            manager.Hande(
                new PositionAcquired(2.90, TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(17)));
            manager.Hande(new PriceUpdated(2.90));
            manager.Hande(new PriceUpdated(2.80));
            manager.Hande(new PriceUpdated(2.70));

            publisher.PublishedMessages.Clear();

            // Act
            manager.Hande(new RemoveMoveUpPoint(2.90));
            manager.Hande(new RemoveMoveUpPoint(2.80));

            // Assert
            Assert.AreEqual(0, publisher.PublishedMessages.Count, "expected no messages");
        }

        [TestMethod]
        public void RemoveMoveUpPointPrice_ShouldChangeTheTreshold()
        {
            // Arrange
            FakePublisher publisher = new FakePublisher();
            TrailingStopLess manager = new TrailingStopLess(publisher);
            manager.Hande(
                new PositionAcquired(2.90, TimeSpan.FromSeconds(11), TimeSpan.FromSeconds(17)));
            manager.Hande(new PriceUpdated(2.90));
            manager.Hande(new PriceUpdated(3.05));
            manager.Hande(new PriceUpdated(3.10));

            publisher.PublishedMessages.Clear();

            // Act
            manager.Hande(new RemoveMoveUpPoint(2.90));
            manager.Hande(new RemoveMoveUpPoint(3.05));

            // Assert
            Assert.AreEqual(1, publisher.PublishedMessages.Count, "expected no messages");
        }

    }
}
