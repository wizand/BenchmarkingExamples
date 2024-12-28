using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace BenchmarkConsole
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public class DictVersusArrayLookup
    {

        [GlobalSetup]
        public void Setup()
        {

            stringToLookfor = "ZEaB";
            stringList = new List<string>();

            stringDict = new Dictionary<string, int>();
            string[] rows = File.ReadAllLinesAsync("10000.txt").GetAwaiter().GetResult();
            stringList.AddRange(rows);

            stringsArray = new string[stringList.Count];
            for (int i = 0; i < stringList.Count; i++)
            {
                stringsArray[i] = stringList[i];
                stringDict.Add(stringList[i], i);
            }

        }

        List<string> stringList;
        Dictionary<string, int> stringDict;
        string[] stringsArray;
        string stringToLookfor;

        [Benchmark]
        public string? FindByIteratingListWithFor()
        {

            for (int i = 0; i < stringList.Count; i++)
            {
                if (stringList[i] == stringToLookfor)
                {
                    return stringList[i];
                }
            }
            return null;

        }

        [Benchmark]
        public string? FindByIteratingListWithForeach()
        {

            foreach( string i in stringList)
            {
                if (i == stringToLookfor)
                {
                    return i;
                }
            }
            return null;

        }

        [Benchmark]
        public string? FindByCheckingIndexFromDict()
        {

            if ( stringDict.TryGetValue(stringToLookfor, out int index) )
            {
                return stringsArray[index];
            }
            return null;

        }

    }
}
