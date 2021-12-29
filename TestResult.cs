using System;

namespace dotnet_perf_test
{
    public class TestResult 
    {
      public string TestName { get; set; }
      public int Run { get; set; }
      public int Items { get; set; }
      public TimeSpan Duration { get; set; }
    }
}
