using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne
{
    public sealed class TrailingStopLess : IHandleMessage<PositionAcquired>, 
        IHandleMessage<PriceUpdated>, IHandleMessage<RemoveMoveUpPoint>, IHandleMessage<RemoveSellingPoint>
    {
        private readonly IMessagePublisher publisher;
        private TimeSpan sustainUp;
        private TimeSpan sustainDown;
        private double threshold;
        private readonly List<double> moveUpPoints;
        private readonly List<double> sellingPoints;

        private bool isAlive;

        public TrailingStopLess(IMessagePublisher publisher)
        {
            this.publisher = publisher;
            sellingPoints = new List<double>();
            moveUpPoints = new List<double>();
        }

        public void Hande(PositionAcquired message)
        {
            sellingPoints.Clear();
            moveUpPoints.Clear();
            sustainUp = message.SustainUp;
            sustainDown = message.SustainDown;
            threshold = message.Threshold;
            isAlive = true;
            publisher.Publish(new ThresholdChanged(threshold));
        }

        public void Hande(PriceUpdated message)
        {
            if (!isAlive)
            {
                publisher.Publish(new AmAlreadyDead());
                return;
            }

            sellingPoints.Add(message.Price);
            moveUpPoints.Add(message.Price);

            publisher.Publish(new SendToMeIn(sustainUp, new RemoveMoveUpPoint(message.Price)));
            publisher.Publish(new SendToMeIn(sustainDown, new RemoveSellingPoint(message.Price)));

            /*
            double down = pricesToSustainUp.Max();

            else if (down < threshold)
            {
                isAlive = false;
                publisher.Publish(new Sell());
            }*/
        }

        public void Hande(RemoveSellingPoint message)
        {
            if (!message.Price.Equals(sellingPoints.First()))
            {
                throw new InvalidOperationException("Wrong price. Can't remove");
            }

            double down = sellingPoints.Max();

            if (down < threshold)
            {
                isAlive = false;
                publisher.Publish(new Sell());
            }
            sellingPoints.RemoveAt(0);
        }

        public void Hande(RemoveMoveUpPoint message)
        {
            if (!message.Price.Equals(moveUpPoints.First()))
            {
                throw new InvalidOperationException("Wrong price. Can't remove");
            }

            double up = moveUpPoints.Min();
            if (up > threshold + 0.1)
            {
                threshold = up - 0.1;
                publisher.Publish(new ThresholdChanged(threshold));
            }
            moveUpPoints.RemoveAt(0);
        }
    }
}
