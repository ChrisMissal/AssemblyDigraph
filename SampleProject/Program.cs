using System.Reflection;
using AssemblyDigraph;

namespace SampleProject;

internal class Program
{
    static void Main(string[] args)
    {
        //var graph = new DirectedGraph<string>();
        //graph.AddEdge("A", "B");
        //graph.AddEdge("A", "C");
        //graph.AddEdge("B", "D");

        //// Test JSON serialization
        //var jsonOutput = graph.ToJson();
        //Console.WriteLine("JSON Output:");
        //Console.WriteLine(jsonOutput);

        //// Test DOT serialization
        //var dotOutput = graph.ToDot();
        //Console.WriteLine("\nDOT Output:");
        //Console.WriteLine(dotOutput);

        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Build the graph
        var assemblyGraph = assemblies.ToDigraph(config =>
        {
            config.ExcludedNamespaces.Add("System"); // Exclude System namespace
            config.IncludeBaseTypeRelationships = true;
            config.IncludeInterfaceRelationships = true;
        });

        // Export as JSON
        var jsonRepresentation = assemblyGraph.ToJson(type => type.FullName ?? type.Name);

        // Output or save the JSON representation
        Console.WriteLine("JSON Output:");
        Console.WriteLine(jsonRepresentation);

        // Export as DOT
        var dotRepresentation = assemblyGraph.ToDot(type => type.FullName ?? type.Name);

        // Output or save the DOT representation
        Console.WriteLine("\nDOT Output:");
        Console.WriteLine(dotRepresentation);
    }
}
