using BenchmarkDotNet.Attributes;

namespace BenchmarkConsole
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public class HashSetToArray
    {

        [GlobalSetup]
        public void Setup()
        {
            stringSet = new HashSet<string>();
            string[] rows = File.ReadAllLinesAsync("10000.txt").GetAwaiter().GetResult();
            stringListWithDuplicates = new(rows.Length * 2);
            stringListWithDuplicates.AddRange(rows);
            stringListWithDuplicates.AddRange(rows);
            stringSetPopulated = new(stringListWithDuplicates);


        }

        HashSet<string> stringSet;
        List<string> stringListWithDuplicates;
        HashSet<string> stringSetPopulated;
        string[] uniqueValues;

        [Benchmark]
        public string[] BuildUniqueValuesWithSet()
        {
            stringSet = new HashSet<string>(stringListWithDuplicates);
            uniqueValues = stringSet.ToArray();
            return uniqueValues;
        }

        [Benchmark]
        public string[] BuildUniqueValuesDumbComparison()
        {
            List<string> uniqueStringsList = new();
            for ( int i = 0; i < stringListWithDuplicates.Count; i++)
            {
                if (uniqueStringsList.Contains(stringListWithDuplicates[i]))
                {
                    continue;
                }
                uniqueStringsList.Add(stringListWithDuplicates[i]);
            }
            uniqueValues = uniqueStringsList.ToArray();
            return uniqueValues;
        }



        [Benchmark]
        public int IterateHashSetWithForeach()
        {

            int count = 0;
            foreach (string s in stringSetPopulated)
            {
                count++;
            }

            return count;

        }

        [Benchmark]
        public int IterateHashSetViaArrayConversion()
        {

            int count = 0;

            var array = stringSetPopulated.ToArray();
            for ( int i = 0; i < array.Length; i++)
            {
                count++;
            }
            return count;

        }

    }
}
