using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne
{
    public class ThresholdChanged : IMessage
    {
        public double Threshold { get; set; }

        public ThresholdChanged(double threshold)
        {
            Threshold = threshold;
        }
    }
}
