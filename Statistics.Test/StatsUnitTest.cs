using System;
using Xunit;
using Statistics;
using System.Collections.Generic;

namespace Statistics.Test
{
    public class StatsUnitTest
    {
        [Fact]
        public void ReportsAverageMinMax()
        {
            StatsComputer statsComputer = new StatsComputer();
            var computedStats = statsComputer.CalculateStatistics(new List<double> { 1.5, 8.9, 3.2, 4.5});
            float epsilon = 0.001F;
            Assert.True(Math.Abs(statsComputer.average - 4.525) <= epsilon);
            Assert.True(Math.Abs(statsComputer.max - 8.9) <= epsilon);
            Assert.True(Math.Abs(statsComputer.min - 1.5) <= epsilon);
        }

        [Fact]
        public void ReportsNaNForEmptyInput()
        {
            var statsComputer = new StatsComputer();
            bool computedStats = statsComputer.CalculateStatistics(new List<double>{});
            Assert.True(computedStats);
            //All fields of computedStats (average, max, min) must be
            //Double.NaN (not-a-number), as described in
            //https://docs.microsoft.com/en-us/dotnet/api/system.double.nan?view=netcore-3.1
        }
        
        [Fact]
        public void RaisesAlertsIfMaxIsMoreThanThreshold()
        {
            EmailAlert emailAlert = new EmailAlert();
            LEDAlert ledAlert = new LEDAlert();

            IAlerter[] alerters ={ emailAlert, ledAlert };

            const float maxThreshold = 10.2f;
            StatsAlerter statsAlerter = new StatsAlerter(maxThreshold, alerters);
            statsAlerter.checkAndAlert(new List<float> { 0.2f, 11.9f, 4.3f, 8.5f });

            Assert.True(emailAlert.emailSent);
            Assert.True(ledAlert.ledGlows);
        }
    }
}
