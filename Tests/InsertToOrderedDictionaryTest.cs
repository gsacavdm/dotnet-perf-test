using System;
using System.Collections.Specialized;

namespace dotnet_perf_test.Tests
{
    public class InsertToOrderedDictionaryTest : PerformanceTest
    {
      public int Limit { get; set; }

      private readonly OrderedDictionary TheList = new OrderedDictionary();

      public override void Test()
      {
        for (var i = 0; i < this.Limit; i++)
        {
          var tmp = i.ToString();
          this.TheList.Add(tmp, tmp);
        }
      }
    }
}
