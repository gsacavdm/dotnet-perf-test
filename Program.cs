using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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

            Console.WriteLine($"Running test {options.Test}");
            var stopwatch = new Stopwatch();

            for (var i = 0; i < options.Iterations; i++)
            {
              var test = (PerformanceTest)(Activator.CreateInstance(null, "dotnet_perf_test.Tests." + options.Test).Unwrap());
              test.PreTest(options);
  
              stopwatch.Start();
              test.Test();
              stopwatch.Stop();
  
              test.PostTest();
  
              Console.WriteLine($"Test Duration: {stopwatch.Elapsed}");
            }
        }
    }
}
