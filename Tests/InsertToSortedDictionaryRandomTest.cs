using System;
using System.Collections.Generic;

namespace dotnet_perf_test.Tests
{
    public class InsertToSortedDictionaryRandomTest : PerformanceTest
    {
      public string[] Keys;

      private readonly SortedDictionary<string,string> TheList = new SortedDictionary<string, string>();

      public override void PreTest(Options options)
      {
        base.PreTest(options);

        this.Keys = new string[this.Options.Items];
        for (var i = 0; i < this.Options.Items; i++)
        {
          this.Keys[i] = Guid.NewGuid().ToString();
        }
      }
      public override void Test()
      {
        for (var i = 0; i < this.Options.Items; i++)
        {
          var tmp = i.ToString();
          this.TheList.Add(this.Keys[i], tmp);
        }
      }
    }
}
