using GenericDirectedGraph;

namespace AssemblyDigraph;

public static class GraphFilterExtensions
{
    /// <summary>
    /// Filters the graph based on the given configuration.
    /// </summary>
    /// <param name="graph">The full graph to filter.</param>
    /// <param name="configure">Optional configuration for customizing the output.</param>
    /// <returns>A filtered directed graph.</returns>
    internal static DirectedGraph<Type> ApplyConfiguration(this DirectedGraph<Type> graph, Action<AnalyzerConfiguration> configure = null)
    {
        var config = new AnalyzerConfiguration();
        configure?.Invoke(config);

        var filteredGraph = new DirectedGraph<Type>();

        // Filter nodes
        foreach (var node in graph.GetNodes())
        {
            if (node == null) continue;
            if (!ShouldIncludeType(node, config)) continue;

            filteredGraph.AddNode(node);
        }

        // Filter edges
        foreach (var (from, to) in graph.GetEdges())
        {
            if (filteredGraph.ContainsNode(from) && filteredGraph.ContainsNode(to))
            {
                filteredGraph.AddEdge(from, to);
            }
        }

        return filteredGraph;
    }

    private static bool ShouldIncludeType(Type type, AnalyzerConfiguration config)
    {
        // Exclude non-public types if not configured to include them
        if (!config.IncludeNonPublicTypes && !(type.IsPublic || type.IsNestedPublic))
            return false;

        // Exclude primitive types if not configured to include them
        if (!config.IncludePrimitiveTypes && type.IsPrimitive)
            return false;

        // Exclude types from excluded namespaces
        if (config.ExcludedNamespaces.Any(ns => type.Namespace?.StartsWith(ns) == true))
            return false;

        // Include only types from included namespaces if specified
        if (config.IncludedNamespaces.Any() && !config.IncludedNamespaces.Any(ns => type.Namespace?.StartsWith(ns) == true))
            return false;

        // Exclude compiler-generated and anonymous-like types
        if (type.Name.Contains("<") || type.Name.Contains("AnonymousType"))
            return false;

        return true;
    }
}
