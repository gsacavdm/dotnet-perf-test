using System;
using System.Collections.Generic;

namespace dotnet_perf_test.Tests
{
    public class InsertToListTest : PerformanceTest
    {
      public int Limit { get; set; }

      private readonly List<string> TheList = new List<string>();

      public override void Test()
      {
        for (var i = 0; i < this.Limit; i++)
        {
          this.TheList.Add(i.ToString());
        }
      }
    }
}
