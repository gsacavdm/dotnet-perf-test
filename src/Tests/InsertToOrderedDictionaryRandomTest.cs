using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace dotnet_perf_test.Tests
{
    public class InsertToOrderedDictionaryRandomTest : PerformanceTest
    {
      public string[] Keys;

      private readonly OrderedDictionary TheList = new OrderedDictionary();

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
