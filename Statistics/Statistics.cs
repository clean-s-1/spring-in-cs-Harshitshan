using System;
using System.Collections.Generic;
using System.Linq;

namespace Statistics
{
    public class StatsComputer
    {
        public double average { get; set; }
        public double max { get; set; }
        public double min { get; set; }

        public bool CalculateStatistics(List<double> numbers) {
            if (numbers.Count != 0)
            {
                min = numbers.Min();
                max = numbers.Max();
                average = numbers.Average();
            }

            return double.IsNaN(numbers.Sum()/numbers.Count);
        }
    }


    public interface IAlerter
    {
        void Alert();
    }

    public class EmailAlert : IAlerter
    {
        public bool emailSent { get; set; }

        public void Alert() {
            // steps to send email
            emailSent = true;
        }

    }

    public class LEDAlert : IAlerter
    {
        public bool ledGlows { get; set; }

        public void Alert() {
            //steps to glo led..
            this.ledGlows = true;
       }
    }

    public class StatsAlerter
    {
        public float maxThreshold { get; set; }
        IEnumerable<IAlerter> items;
        public StatsAlerter(float maxThreshold, IEnumerable<IAlerter> items)
        { 
            this.maxThreshold = maxThreshold;
            this.items = items;
        }

        public void checkAndAlert(List<float> values) {
            float max = values.Max();
            if (max > maxThreshold)
            {
                foreach(IAlerter item in items)   {
                    item.Alert();
                }             
            }
        }
    }
}
