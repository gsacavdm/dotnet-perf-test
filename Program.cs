using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommandLine;

namespace dotnet_perf_test
{
    class Program
    {
        public class Options
        {
          [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
          public bool Verbose { get; set; }
          
          [Option('l', "limit", Required = false, Default=10, HelpText = "TBD")]
          public int Limit { get; set; }

          [Option('i', "iterations", Required = false, Default=1, HelpText = "Test iterations")]
          public int Iterations { get; set; }
        }

        static Options ProgramOptions { get; set; }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed<Options>(o => 
              {
                ProgramOptions = o;
              });

            Console.WriteLine("Hello World!");

            var stopwatch = new Stopwatch();

            for (var i = 0; i < ProgramOptions.Iterations; i++)
            {
              var test = new InsertToListTest { Limit = ProgramOptions.Limit };
              test.PreTest();
  
              stopwatch.Start();
              test.Test();
              stopwatch.Stop();
  
              test.PostTest();
  
              Console.WriteLine($"Test Duration: {stopwatch.Elapsed}");
            }
        }
    }

    public abstract class PerformanceTest
    {
      public virtual void PreTest() {}
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
}
