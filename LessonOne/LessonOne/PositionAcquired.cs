using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne
{
    public sealed class PositionAcquired : IMessage
    {
        public PositionAcquired(double threshold, TimeSpan sustainUp, TimeSpan sustainDown)
        {
            Threshold = threshold;
            SustainUp = sustainUp;
            SustainDown = sustainDown;
        }

        public TimeSpan SustainUp { get; private set; }

        public TimeSpan SustainDown { get; private set; }
        public double Threshold { get; private set; }
    }
}
