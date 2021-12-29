using System;
using CommandLine;

namespace dotnet_perf_test
{
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
}
