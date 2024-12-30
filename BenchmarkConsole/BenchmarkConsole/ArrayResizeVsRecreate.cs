using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace BenchmarkConsole
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public  class ArrayResizeVsRecreate
    {
        [GlobalSetup]
        public void Setup()
        {

             string[] rows = File.ReadAllLinesAsync("100000.txt").GetAwaiter().GetResult();
            //string[] rows = File.ReadAllLinesAsync("100.txt").GetAwaiter().GetResult();
            List<string> stringListWithDuplicates = new(rows.Length * 2);
            stringListWithDuplicates.AddRange(rows);
            stringListWithDuplicates.AddRange(rows);
            originalArray = stringListWithDuplicates.ToArray();
            originalArrayForResize = stringListWithDuplicates.ToArray();

        }

     
        string[] originalArray;
        string[] originalArrayForResize;


        //Still faster and more memory efficient with 2*100k elements. 
        [Benchmark]
        public string[] AddNewStringWithRecreateAndCopy()
        {
            string[] newOneLongerArray = new string[originalArray.Length + 1];
            Array.Copy(originalArray, newOneLongerArray, originalArray.Length);
            newOneLongerArray[originalArray.Length] = "NewString";

            return newOneLongerArray;
        }

        [Benchmark]
        public string[] AddNewStringWithArrayResize()
        {
            
            Array.Resize(ref originalArrayForResize, originalArrayForResize.Length+1);
            originalArrayForResize[originalArrayForResize.Length-1] = "NewString";

            return originalArrayForResize;
        }

        [Benchmark]
        public string[] AddNewStringWithLinqAppend()
        {

            string[] appendedArray = originalArray.Append("NewString").ToArray();

            return appendedArray;
        }
    }
}
