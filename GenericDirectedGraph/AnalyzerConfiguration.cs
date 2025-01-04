namespace AssemblyDigraph;

public class AnalyzerConfiguration
{
    /// <summary>
    /// Whether to include non-public types. Default is true.
    /// </summary>
    public bool IncludeNonPublicTypes { get; set; } = true;

    /// <summary>
    /// Whether to include primitive types in the graph. Default is false.
    /// </summary>
    public bool IncludePrimitiveTypes { get; set; } = false;

    /// <summary>
    /// A list of namespaces to include in the analysis. If empty, all namespaces are included.
    /// </summary>
    public List<string> IncludedNamespaces { get; set; } = new();

    /// <summary>
    /// A list of namespaces to exclude from the analysis. If empty, no namespaces are excluded.
    /// </summary>
    public List<string> ExcludedNamespaces { get; set; } = new();

    /// <summary>
    /// Whether to include relationships to base types. Default is true.
    /// </summary>
    public bool IncludeBaseTypeRelationships { get; set; } = true;

    /// <summary>
    /// Whether to include relationships to implemented interfaces. Default is true.
    /// </summary>
    public bool IncludeInterfaceRelationships { get; set; } = true;

    /// <summary>
    /// Whether to include generic type arguments in the graph. Default is true.
    /// </summary>
    public bool IncludeGenericArguments { get; set; } = true;
}
