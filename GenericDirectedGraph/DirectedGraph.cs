using System.Text;
using System.Text.Json;

namespace GenericDirectedGraph;

public class DirectedGraph<T>
{
    private readonly Dictionary<T, HashSet<T>> _adjacencyList = new();

    /// <summary>
    /// Checks if the graph contains the specified node.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>True if the node exists in the graph; otherwise, false.</returns>
    public bool ContainsNode(T node)
    {
        return _adjacencyList.ContainsKey(node);
    }

    public void AddNode(T node)
    {
        if (!_adjacencyList.ContainsKey(node))
            _adjacencyList[node] = new HashSet<T>();
    }

    public void AddEdge(T from, T to)
    {
        if (!_adjacencyList.ContainsKey(from))
            AddNode(from);
        if (!_adjacencyList.ContainsKey(to))
            AddNode(to);

        _adjacencyList[from].Add(to);
    }

    public IEnumerable<T> GetNodes() => _adjacencyList.Keys;

    public IEnumerable<T> GetChildren(T node) =>
        _adjacencyList.ContainsKey(node) ? _adjacencyList[node] : Array.Empty<T>();

    public IEnumerable<(T From, T To)> GetEdges()
    {
        foreach (var from in _adjacencyList.Keys)
        {
            foreach (var to in _adjacencyList[from])
            {
                yield return (from, to);
            }
        }
    }
    /// <summary>
    /// Exports the graph to DOT format for visualization with Graphviz.
    /// </summary>
    public string ToDot(Func<T, string>? nodeFormatter = null)
    {
        nodeFormatter ??= node => node?.ToString() ?? "null";
        var sb = new StringBuilder();
        sb.AppendLine("digraph G {");

        foreach (var (from, children) in _adjacencyList)
        {
            foreach (var to in children)
            {
                sb.AppendLine($"    \"{nodeFormatter(from)}\" -> \"{nodeFormatter(to)}\";");
            }
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    /// <summary>
    /// Exports the graph to a JSON structure compatible with D3.js.
    /// </summary>
    public string ToJson(Func<T, string>? nodeFormatter = null)
    {
        nodeFormatter ??= node => node?.ToString() ?? "null";

        var nodes = _adjacencyList.Keys.Select(node => new { id = nodeFormatter(node) }).ToList();
        var links = _adjacencyList
            .SelectMany(kvp => kvp.Value.Select(child => new
            {
                source = nodeFormatter(kvp.Key),
                target = nodeFormatter(child)
            }))
            .ToList();

        var graphData = new { nodes, links };
        return JsonSerializer.Serialize(graphData, new JsonSerializerOptions { WriteIndented = true });
    }
}
