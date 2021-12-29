using System;
using System.Collections.Generic;

namespace dotnet_perf_test.Tests
{
    public class InsertToSortedDictionaryTest : PerformanceTest
    {
      private readonly SortedDictionary<string,string> TheList = new SortedDictionary<string, string>();

      public override void Test()
      {
        for (var i = 0; i < this.Options.Items; i++)
        {
          var tmp = i.ToString();
          this.TheList.Add(tmp, tmp);
        }
      }
    }
}
