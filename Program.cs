using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using CommandLine;
using dotnet_perf_test.Tests;

namespace dotnet_perf_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = null;
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(o => 
              {
                options = o;
              });

            if (options == null) throw new ArgumentNullException(nameof(options));

            var testResults = new List<TestResult>();

            foreach(var testName in options.Tests.Split(','))
            {
              Console.WriteLine($"Running test {testName}");
              var stopwatch = new Stopwatch();
  
              for (var run = 0; run < options.Runs; run++)
              {
                var test = (PerformanceTest)(Activator.CreateInstance(null, "dotnet_perf_test.Tests." + testName).Unwrap());
                test.PreTest(options);
    
                stopwatch.Start();
                test.Test();
                stopwatch.Stop();
    
                test.PostTest();
    
                testResults.Add(
                  new TestResult
                  {
                    TestName = testName,
                    Run = run,
                    Items = options.Items,
                    Duration = stopwatch.Elapsed
                  }
                );
              }
            }

            var testSummaries = testResults.GroupBy(t => t.TestName, t => t)
              .Select(g => new {
              TestName = g.Key,
              Runs = g.Count(),
              Average = g.Average(ta => ta.Duration.TotalSeconds)
            });

            foreach(var testSummary in testSummaries)
            {
              Console.WriteLine($"Test {testSummary.TestName} | Runs {testSummary.Runs} | Average (in Seconds) {testSummary.Average}");
            }
        }
    }
}
