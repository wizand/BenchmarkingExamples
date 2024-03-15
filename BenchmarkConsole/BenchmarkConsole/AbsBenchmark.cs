
using BenchmarkDotNet.Attributes;

using Microsoft.Diagnostics.Tracing.Parsers.AspNet;

using System.Text;

[ShortRunJob]
[MemoryDiagnoser]
public class AbsBenchmark
{


    [GlobalSetup]   
    public void Setup()
    {
        floats = new float[1000000000];
        Random r = new Random();
        for (int i = 0; i < floats.Length; i++)
        {
            //Generate random float between -999999 and 999999
            floats[i] = (float)(r.NextDouble() * 1999998 - 999999);
        }
    }

    public float[] floats;

    [Benchmark]
    public float[] GetAbsUsingSystem ()
    {

        float[] absValues = new float[floats.Length];
        for (int i = 0; i < floats.Length; i++)
        {
            absValues[i] = Math.Abs(floats[i]);
        }

        return absValues;
    }


    [Benchmark]
    public float[] GetAbsUsingCustom()
    {

        float[] absValues = new float[floats.Length];
        for (int i = 0; i < floats.Length; i++)
        {
            if (floats[i] < 0)
            {
                absValues[i] = -floats[i];
            } else
            {
                absValues[i] = floats[i];
            }
        }

        return absValues;
    }



}
