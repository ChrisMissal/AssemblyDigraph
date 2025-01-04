# AssemblyDigraph

**AssemblyDigraph** is a fun and exploratory project designed to analyze .NET assemblies and visualize their type relationships as a directed graph. It provides insights into the inner workings of your code by mapping connections between types, including methods, fields, interfaces, and more.

While this project started as a playful experiment, it's now open for collaboration! Whether you're interested in contributing new features, optimizing the graphing engine, or just exploring your codebase in a new way, you're welcome to join.

---

## Features

- **Comprehensive Graph Generation**:
  - Builds a directed graph of types, their relationships, and dependencies.
  - Includes base types, interfaces, method arguments, and field types.
  
- **Dynamic Filtering**:
  - Configurable options to exclude anonymous types, non-public members, primitives, or specific namespaces.
  - Apply filters dynamically while exporting graphs.

- **Graph Exports**:
  - Export graphs as:
    - **JSON**: Ideal for use with tools like D3.js.
    - **DOT (Graphviz)**: Perfect for generating static or interactive visualizations.

---

## Example Usage

### Analyze an Assembly and Export as JSON

```csharp
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
```

