using System;
using System.Collections.Generic;

namespace dotnet_perf_test.Tests
{
    public class InsertToListTest : PerformanceTest
    {
      private readonly List<string> TheList = new List<string>();

      public override void Test()
      {
        for (var i = 0; i < this.Options.Items; i++)
        {
          this.TheList.Add(i.ToString());
        }
      }
    }
}
