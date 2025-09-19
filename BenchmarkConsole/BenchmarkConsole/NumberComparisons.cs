using System.Runtime.ExceptionServices;
using BenchmarkDotNet.Attributes;
namespace BenchmarkConsole
{


    [ShortRunJob]
    [MemoryDiagnoser]
    public class NumberComparisons
    {


        [GlobalSetup]
        public void Setup()
        {
            string[] rows = File.ReadAllLinesAsync("10000_small_doubles.txt").GetAwaiter().GetResult();
            //sampleSet = new double[rows.Length];
            sampleSet = rows.Select(r => double.Parse(r)).ToArray();
            // Initialize 2D array of 10,000 pairs (requirement) mixing exact, near, and far cases
            int pairCount = 10000;
            pairSamples = new double[pairCount, 2];
            for (int i = 0; i < pairCount; i++)
            {
                double baseValue = sampleSet[i % sampleSet.Length];
                double offset;
                switch (i % 3)
                {
                    case 0: offset = 0.0; break; // exact equal
                    case 1: offset = 1e-6; break; // within default tolerance (1e-6 < 1e-5)
                    default: offset = 1e-4; break; // outside default tolerance (1e-4 > 1e-5)
                }
                pairSamples[i, 0] = baseValue;
                pairSamples[i, 1] = baseValue + offset;
            }
            equalityCount = 0;
        }

        List<double> determinedToBeZero = new List<double>();
    double[] sampleSet = Array.Empty<double>();
    double[,] pairSamples = new double[0, 0]; // holds (A,B) value pairs for equality comparisons
    int equalityCount; // accumulator to keep benchmark work from being optimized away

        [Benchmark]
        public void B_IsBasicallyZeroCheckInbeteen()
        {
            for (int i = 0; i < sampleSet.Length; i++)
            {
                if (IsBasicallyZeroCheckInbeteen(sampleSet[i]))
                {
                    determinedToBeZero.Add(sampleSet[i]);
                }
            }
        }

        [Benchmark]
        public void B_IsBasicallyZeroCheckInbeteenNoZeroCheck()
        {
            for (int i = 0; i < sampleSet.Length; i++)
            {
                if (IsBasicallyZeroCheckInbeteenNoZeroCheck(sampleSet[i]))
                {
                    determinedToBeZero.Add(sampleSet[i]);
                }
            }
        }

        [Benchmark]
        public void B_IsBasicallyZeroCheckAbsUnderValueNoZeroCheck()
        {
            for (int i = 0; i < sampleSet.Length; i++)
            {
                if (IsBasicallyZeroCheckAbsUnderValueNoZeroCheck(sampleSet[i]))
                {
                    determinedToBeZero.Add(sampleSet[i]);
                }
            }
        }

        [Benchmark]
        public void B_IsBasicallyZeroCheckAbsUnderValue()
        {
            for (int i = 0; i < sampleSet.Length; i++)
            {
                if (IsBasicallyZeroCheckAbsUnderValue(sampleSet[i]))
                {
                    determinedToBeZero.Add(sampleSet[i]);
                }
            }
        }

        public bool IsBasicallyZeroCheckInbeteen(double valueToCheck)
        {
            if (valueToCheck == 0)
            {
                return true;
            }

            if (valueToCheck > -0.00001 && valueToCheck < 0.00001)
            {
                return true;
            }

            return false;
        }

        public bool IsBasicallyZeroCheckInbeteenNoZeroCheck(double valueToCheck)
        {
            if (valueToCheck > -0.00001 && valueToCheck < 0.00001)
            {
                return true;
            }

            return false;
        }

        public bool IsBasicallyZeroCheckAbsUnderValue(double valueToCheck)
        {
            if (valueToCheck == 0)
            {
                return true;
            }

            if (Math.Abs(valueToCheck) < 0.00001)
            {
                return true;
            }

            return false;
        }

        public bool IsBasicallyZeroCheckAbsUnderValueNoZeroCheck(double valueToCheck)
        {
            if (Math.Abs(valueToCheck) < 0.00001)
            {
                return true;
            }

            return false;
        }

        public bool IsDoubleEqualTo(double doubleValue, double compareTo, int precision)
        {
            if (doubleValue == compareTo)
            {
                return true;
            }
            if (precision == 0)
            {
                return doubleValue == compareTo;
            }
            double precisionBuffer = Math.Pow(10, -1 * precision);
            double highLimit = compareTo + precisionBuffer;
            double lowLimit = compareTo - precisionBuffer;
            if (doubleValue > highLimit)
            {
                return false;
            }
            if (doubleValue < lowLimit)
            {
                return false;
            }
            return true;
        }

        public bool IsDoubleEqualWithTolerance(double valueA, double valueB, double tolerance = 0.00001)
        {
            double difference = Math.Abs(valueA - valueB);
            if (difference <= tolerance)
            {
                return true;
            }
            return false;
        }

        [Benchmark]
        public void B_IsDoubleEqualWithTolerance()
        {
            int len = pairSamples.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                if (IsDoubleEqualWithTolerance(pairSamples[i, 0], pairSamples[i, 1]))
                {
                    equalityCount++;
                }
            }
        }

        [Benchmark]
        public void B_IsDoubleEqualToPrecision5()
        {
            int len = pairSamples.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                if (IsDoubleEqualTo(pairSamples[i, 0], pairSamples[i, 1], 5))
                {
                    equalityCount++;
                }
            }
        }
    }
}