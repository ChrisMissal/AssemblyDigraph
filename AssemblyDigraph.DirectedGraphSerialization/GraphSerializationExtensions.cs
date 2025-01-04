using System.Text;
using System.Text.Json;
using GenericDirectedGraph;

namespace DirectedGraphSerialization
{
    //public static class GraphSerializationExtensions
    //{
    //    /// <summary>
    //    /// Serializes the directed graph to a JSON structure.
    //    /// </summary>
    //    /// <typeparam name="T">The type of the graph nodes.</typeparam>
    //    /// <param name="graph">The directed graph to serialize.</param>
    //    /// <param name="nodeFormatter">Optional function to format node identifiers.</param>
    //    /// <returns>A JSON string representing the graph.</returns>
    //    public static string ToJson<T>(this DirectedGraph<T> graph, Func<T, string>? nodeFormatter = null)
    //    {
    //        nodeFormatter ??= node => node?.ToString() ?? "null";

    //        var nodes = graph.GetNodes().Select(node => new { id = nodeFormatter(node) }).ToList();
    //        var links = graph.GetEdges()
    //            .Select(edge => new
    //            {
    //                source = nodeFormatter(edge.From),
    //                target = nodeFormatter(edge.To)
    //            })
    //            .ToList();

    //        var graphData = new { nodes, links };
    //        return JsonSerializer.Serialize(graphData, new JsonSerializerOptions { WriteIndented = true });
    //    }

    //    /// <summary>
    //    /// Serializes the directed graph to the DOT format.
    //    /// </summary>
    //    /// <typeparam name="T">The type of the graph nodes.</typeparam>
    //    /// <param name="graph">The directed graph to serialize.</param>
    //    /// <param name="nodeFormatter">Optional function to format node identifiers.</param>
    //    /// <returns>A DOT string representing the graph.</returns>
    //    public static string ToDot<T>(this DirectedGraph<T> graph, Func<T, string>? nodeFormatter = null)
    //    {
    //        nodeFormatter ??= node => node?.ToString() ?? "null";

    //        var sb = new StringBuilder();
    //        sb.AppendLine("digraph G {");

    //        foreach (var (from, to) in graph.GetEdges())
    //        {
    //            sb.AppendLine($"    \"{nodeFormatter(from)}\" -> \"{nodeFormatter(to)}\";");
    //        }

    //        sb.AppendLine("}");
    //        return sb.ToString();
    //    }
    //}
}
