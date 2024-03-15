
using BenchmarkDotNet.Attributes;

using System.Text;

[ShortRunJob]
[MemoryDiagnoser]
public class StringBenchmark
{

    [Benchmark]
    public string ConcatStringsWithPlus()
    {

        string result = "";
        for (int i = 0; i < 1000; i++)
        {
            result = result + i;
        }

        return result;
    }

    [Benchmark]
    public string ConcatStringsWithStringBuilder()
    {

        StringBuilder result = new StringBuilder();
        for (int i = 0; i < 1000; i++)
        {
            result.Append(i);
        }

        return result.ToString();
    }



}
