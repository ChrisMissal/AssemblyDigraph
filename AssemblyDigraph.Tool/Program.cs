using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AssemblyDigraph;
using GenericDirectedGraph;

namespace AssemblyDigraph.Tool;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: dotnet-assemblydigraph <build-output-folder> <assembly-name>");
            return;
        }

        var buildOutputFolder = args[0];
        var assemblyName = args[1];

        Console.WriteLine($"[INFO] Starting AssemblyDigraph in folder: {buildOutputFolder}");
        Console.WriteLine($"[INFO] Looking for assembly named: {assemblyName}");

        // Step 1: Locate the assembly
        Console.WriteLine("[INFO] Searching for assembly file...");
        var matchingAssemblies = Directory.GetFiles(buildOutputFolder, "*.dll", SearchOption.AllDirectories)
            .Where(file => Path.GetFileName(file).Equals(assemblyName, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(File.GetLastWriteTime)
            .ToList();

        if (matchingAssemblies.Count == 0)
        {
            Console.WriteLine($"[ERROR] No assembly matching '{assemblyName}' found in the specified folder.");
            return;
        }

        if (matchingAssemblies.Count > 1)
        {
            Console.WriteLine($"[WARNING] Multiple assemblies named '{assemblyName}' found. Using the most recent one.");
            foreach (var file in matchingAssemblies.Skip(1))
            {
                Console.WriteLine($"[INFO] Additional match: {file}");
            }
        }

        var assemblyFile = matchingAssemblies.First();
        Console.WriteLine($"[INFO] Using assembly: {assemblyFile}");

        // Step 2: Load the assembly and build the graph
        Console.WriteLine("[INFO] Loading the assembly...");
        var assembly = Assembly.LoadFrom(assemblyFile);

        Console.WriteLine("[INFO] Building the directed graph...");
        var graph = new[] { assembly }.ToDigraph();

        Console.WriteLine("[INFO] Converting graph to JSON...");
        var jsonGraph = graph.ToJson(type => type.FullName ?? type.Name);

        // Step 3: Save the graph to a file
        var outputPath = Path.Combine(buildOutputFolder, "assembly-graph.json");
        Console.WriteLine($"[INFO] Saving graph to file: {outputPath}");
        await File.WriteAllTextAsync(outputPath, jsonGraph);

        Console.WriteLine("[INFO] Graph saved successfully.");
    }
}