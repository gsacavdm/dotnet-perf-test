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
            var testDurations = new Dictionary<string, List<double>>();
            var stopwatch = new Stopwatch();

            foreach(var testName in options.Tests.Split(','))
            {
              Console.WriteLine($"Running test {testName} {options.Runs} times with {options.Items} items");
              testDurations.Add(testName, new List<double>());

              for (var run = 0; run < options.Runs; run++)
              {
                var test = (PerformanceTest)(Activator.CreateInstance(null, "dotnet_perf_test.Tests." + testName).Unwrap());
                test.PreTest(options);
                
                stopwatch.Reset();
                stopwatch.Start();
                test.Test();
                stopwatch.Stop();
    
                test.PostTest();
  
                var duration = stopwatch.Elapsed.TotalSeconds;

                testResults.Add(
                  new TestResult
                  {
                    TestName = testName,
                    Run = run,
                    Items = options.Items,
                    Duration = duration                  }
                );

                testDurations[testName].Add(duration);
              }

              testDurations[testName].Sort();
            }

            Console.WriteLine("====== Test Results =======");
            int percentile90Index = (int)(options.Runs * .9);


            var testSummaries = testResults
              .GroupBy(t => t.TestName, t => t)
              .Select(g => new {
                TestName = g.Key,
                Runs = g.Count(),
                //DurationsUnsorted = String.Join(',',  g.Select(gg => gg.Duration)),
                //DurationsSorted = String.Join(',', testDurations[g.Key]),
                Percentile90 = testDurations[g.Key].ElementAt(percentile90Index),
                Average = g.Average(ta => ta.Duration)
              });

            foreach(var testSummary in testSummaries)
            {
              Console.WriteLine($"Test {testSummary.TestName} | Runs {testSummary.Runs} | Average (in Seconds) {testSummary.Average} | Percentile90 {testSummary.Percentile90}"); //Durations {testSummary.Durations}");
            }
        }
    }
}
