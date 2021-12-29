using System;
using System.Collections.Generic;

namespace dotnet_perf_test.Tests
{
    public abstract class PerformanceTest
    {
      protected Options Options { get; set; }

      public virtual void PreTest(Options options)
      {
        this.Options = options;
      }

      public abstract void Test();
      public virtual void PostTest() {}
    }
}
