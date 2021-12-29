using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using CommandLine;

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
              var test = (PerformanceTest)(Activator.CreateInstance(null, "dotnet_perf_test." + options.Test).Unwrap());
              test.PreTest(options);
  
              stopwatch.Start();
              test.Test();
              stopwatch.Stop();
  
              test.PostTest();
  
              Console.WriteLine($"Test Duration: {stopwatch.Elapsed}");
            }
        }
    }

    public class Options
    {
      [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
      public bool Verbose { get; set; }
      
      [Option('l', "limit", Required = false, Default=10, HelpText = "TBD")]
      public int Limit { get; set; }

      [Option('i', "iterations", Required = false, Default=1, HelpText = "Test iterations")]
      public int Iterations { get; set; }

      [Option('t', "test", Required = true, HelpText = "Name of test to run")]
      public string Test { get; set; }
    }

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

    public class InsertToOrderedDictionaryTest : PerformanceTest
    {
      public int Limit { get; set; }

      private readonly OrderedDictionary TheList = new OrderedDictionary();

      public override void Test()
      {
        for (var i = 0; i < this.Limit; i++)
        {
          var tmp = i.ToString();
          this.TheList.Add(tmp, tmp);
        }
      }

    }
}
