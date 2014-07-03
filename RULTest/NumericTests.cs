using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RUL;

namespace RULTest
{

    struct NumTestResult
    {
        public double Max, Average, Min;
    }

    [TestClass]
    public class NumericTests
    {
        private const int TEST_COUNT = 10000000;
        private const float MAX_DEVIATION = 0.05F;

        [TestMethod]
        public void TestInts()
        {
            float lower = -12;
            float upper = 1;

            NumTestResult result = GetNumStats("int", lower, upper);
            if (result.Min < lower)
                Assert.Fail(string.Format("Lower bound exceeded : {0} should be {1}", result.Min, lower));
            else if ((int)result.Min != lower)
                Assert.Inconclusive(string.Format("Lower bound not met : {0} should be {1}", result.Min, lower));
            if(result.Max > upper)
                Assert.Fail(string.Format("Upper bound exceeded : {0} should be {1}", result.Max, upper));
            else if ((int)result.Max != upper)
                Assert.Inconclusive(string.Format("Upper bound not met : {0} should be {1}", result.Max, upper));
            if (!CheckAverage(result.Average, lower, upper))
                Assert.Inconclusive(string.Format("Average is off : {0} should be {1}", result.Average, lower + (upper - lower) / 2));
        }

        [TestMethod]
        public void TestSigns()
        {
            int sum = 0;
            for (int i = 0; i < TEST_COUNT; i++)
                sum += Rul.RandSign();
            if (sum < -20 || sum > 20)
                Assert.Inconclusive(string.Format("Sum is {0}, relative error of {1}", sum,(float)sum / (float)TEST_COUNT));
        }

        [TestMethod]
        public void TestFloats()
        {
            float lower = -4.08F;
            float upper = 11.92F;

            NumTestResult result = GetNumStats("float", lower, upper);
            if (result.Min < lower)
                Assert.Fail(string.Format("Lower bound exceeded : {0} should be {1}", result.Min, lower));
            else if (!AreEqual(result.Min,lower))
                Assert.Inconclusive(string.Format("Lower bound not met : {0} should be {1}", result.Min, lower));
            if (result.Max > upper)
                Assert.Fail(string.Format("Upper bound exceeded : {0} should be {1}", result.Max, upper));
            else if (!AreEqual(result.Max, upper))
                Assert.Inconclusive(string.Format("Upper bound not met : {0} should be {1}", result.Max, upper));
            if (!CheckAverage(result.Average, lower, upper))
                Assert.Inconclusive(string.Format("Average is off : {0} should be {1}", result.Average, lower + (upper - lower) / 2));
        }

        private NumTestResult GetNumStats(string numType, float lower, float upper, InclusionOptions inclusion = InclusionOptions.Both)
        {
            NumTestResult result = new NumTestResult();
            double sum = 0;
            for (int i = 0; i < TEST_COUNT; i++)
            {
                double r = 0;
                switch (numType)
                {
                    case "int":
                        r = Rul.RandInt((int)lower, (int)upper, inclusion);
                        break;
                    case "float":
                        r = Rul.RandFloat(lower, upper);
                        break;
                    case "double":
                        r = Rul.RandDouble(lower, upper);
                        break;
                    default:
                        throw new ArgumentException("Invalid numType");

                }
                sum += r;
                if (i == 0 || r > result.Max)
                    result.Max = r;
                if (i == 0 || r < result.Min)
                    result.Min = r;
            }
            result.Average = sum / (double)TEST_COUNT;
            return result;
        }

        private bool CheckAverage(double average, double lower, double upper)
        {
            double intervalSize = upper - lower;
            double expectedAverage = lower + intervalSize / 2F;
            return (Math.Abs(expectedAverage - average) <= MAX_DEVIATION * intervalSize);
        }

        private bool AreEqual(double a, double b)
        {
            return (Math.Abs(a - b) < 0.00001F);
        }
    }
}
