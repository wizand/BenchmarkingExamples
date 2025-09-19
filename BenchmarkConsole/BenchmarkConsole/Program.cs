using BenchmarkConsole;

using BenchmarkDotNet.Running;


public class Program
{

    public async static Task Main(string[] args)
    {



        // await GenerateDoubleData();

        //GenerateStringData();
        //BenchmarkRunner.Run<StringBenchmark>();
        //BenchmarkRunner.Run<AbsBenchmark>();
        //BenchmarkRunner.Run<DictVersusArrayLookup>();
        //BenchmarkRunner.Run<HashSetToArray>();
        // BenchmarkRunner.Run<ArrayResizeVsRecreate>();
        BenchmarkRunner.Run<NumberComparisons>();


        //ArrayResizeVsRecreate arrayResizeVsRecreate = new ArrayResizeVsRecreate();
        //arrayResizeVsRecreate.Setup();
        //arrayResizeVsRecreate.AddNewStringWithArrayResize();

        //HashSetToArray hashSetToArray = new HashSetToArray();
        //hashSetToArray.Setup();
        //hashSetToArray.IterateHashSetWithForeach();

    }


    public async Task GenerateSringData()
    {
        List<Task> tasks = new List<Task>();
        StringDataGenerator stringDataGenerator = new StringDataGenerator();
        for (int i = 10; i < 1000000; i = i * 10)
        {
            tasks.Add(stringDataGenerator.GenerateWords(i, 2, 7, $"{i}.txt"));
        }


        Task.WaitAll(tasks.ToArray());
    }

    public static async Task GenerateDoubleData()
    {
        List<Task> tasks = new List<Task>();
        DoubleDataGenerator doubleDataGenerator = new DoubleDataGenerator();
        for (int i = 100; i < 10000000; i = i * 10)
        {
            tasks.Add(doubleDataGenerator.GenerateDoubles(i, -0.005, 0.005, $"{i}_small_doubles.txt"));
        }


        Task.WaitAll(tasks.ToArray());
    }
}
