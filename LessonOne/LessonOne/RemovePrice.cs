using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne
{
    public class RemoveMoveUpPoint : IMessage
    {
        public double Price { get; set; }

        public RemoveMoveUpPoint(double price)
        {
            Price = price;
        }
    }
}
