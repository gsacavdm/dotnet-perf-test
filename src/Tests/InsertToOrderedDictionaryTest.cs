using System;
using System.Collections.Specialized;

namespace dotnet_perf_test.Tests
{
    public class InsertToOrderedDictionaryTest : PerformanceTest
    {
      private readonly OrderedDictionary TheList = new OrderedDictionary();

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
