using BenchmarkConsole;

using BenchmarkDotNet.Running;


public class Program
{

    public static void Main(string[] args)
    {



        //GenerateStringData();
        //BenchmarkRunner.Run<StringBenchmark>();
        //BenchmarkRunner.Run<AbsBenchmark>();
        //BenchmarkRunner.Run<DictVersusArrayLookup>();
        //BenchmarkRunner.Run<HashSetToArray>();
        BenchmarkRunner.Run<ArrayResizeVsRecreate>();


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
}