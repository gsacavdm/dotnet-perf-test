using System;
using CommandLine;

namespace dotnet_perf_test
{
    public class Options
    {
      [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
      public bool Verbose { get; set; }
      
      [Option('r', "runs", Required = false, Default=10, HelpText = "Number of times each test is run")]
      public int Runs { get; set; }

      [Option('i', "items", Required = false, Default=100, HelpText = "Number of items the test should use")]
      public int Items { get; set; }

      [Option('t', "tests", Required = true, HelpText = "Comma-delimited list of tests to run")]
      public string Tests { get; set; }
    }
}
