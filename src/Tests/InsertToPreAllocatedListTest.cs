using System;
using System.Collections.Generic;

namespace dotnet_perf_test.Tests
{
    public class InsertToPreAllocatedListTest : PerformanceTest
    {
      private List<int> TheList = new();
      public override void PreTest(Options options)
      {
        this.TheList = new List<int>(options.Items);
        base.PreTest(options);
      }

      public override void Test()
      {
        if (this.TheList == null) throw new Exception("Test hasn't been initialized. Please call PreTest() first before calling Test()");

        for (var i = 0; i < this.Options.Items; i++)
        {
          this.TheList.Add(i);
        }
      }
    }
}
