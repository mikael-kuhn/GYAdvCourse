using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne
{
    public sealed class PriceUpdated : IMessage
    {
        public double Price { get; set; }

        public PriceUpdated(double price)
        {
            Price = price;
        }
    }
}
