using System;
using System.Collections.Generic;

namespace code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

	    var e1 = Environment.GetEnvironmentVariable("COMPlus_gcAllowVeryLargeObjects");
	    var e2 = Environment.GetEnvironmentVariable("DOTNET_gcAllowVeryLargeObjects");

	    Console.WriteLine($"Env COMPlus_gcAllowVeryLargeObjects {e1}");
	    Console.WriteLine($"Env DOTNET_gcAllowVeryLargeObjects {e2}");

	    Console.WriteLine($"Is64BitProcess {Environment.Is64BitProcess}");
	    Console.WriteLine($"Is64BitOperatingSystem {Environment.Is64BitOperatingSystem}");

	    Console.WriteLine($"Size of int: {sizeof(int)}");

	    //return;

	    var partitions 	= 20;
	    var items 		= 500000000;

	    List<string>[] lists = new List<string>[partitions];
	    
	    Console.WriteLine($"Items {items}");
	    Console.WriteLine($"Partitions {partitions}");

	    for(var i=0; i<partitions; i++)
	    {
		    lists[i] = new List<string>();
	    }

	    try
	    {
	    for(var i=0; i<items; i++)
	    {
		    if(i%1000000==0)
		    {
		    	Console.WriteLine(String.Format("Processing item # {0:n}", i));
			Console.WriteLine($"List1 has {lists[0].Count} items");
			Console.WriteLine($"List2 has {lists[1].Count} items");
		    }

		    lists[i%partitions].Add(i.ToString());

		    //Console.WriteLine($"Inserted to Partition {i%partitions}");
	    }
	    }
	    catch (Exception e)
	    {
		    Console.WriteLine($"Exception {e.Message}");
	    }

	    Console.WriteLine("Done!");
        }
    }
}
