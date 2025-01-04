using System.Reflection;
using GenericDirectedGraph;

namespace AssemblyDigraph;

public static class AssemblyGraphExtensions
{
    /// <summary>
    /// Builds a directed graph of types and their references from the given assemblies.
    /// </summary>
    /// <param name="assemblies">A collection of assemblies to analyze.</param>
    /// <returns>A directed graph representing all type relationships.</returns>
    public static DirectedGraph<Type> ToDigraph(this IEnumerable<Assembly> assemblies, Action<AnalyzerConfiguration> configure = null)
    {
        var graph = new DirectedGraph<Type>();

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                // Add the type as a node
                graph.AddNode(type);

                // Add edges for base types
                if (type.BaseType != null && type.BaseType != typeof(object))
                    graph.AddEdge(type, type.BaseType);

                // Add edges for implemented interfaces
                foreach (var @interface in type.GetInterfaces())
                {
                    graph.AddEdge(type, @interface);
                }

                // Add edges for fields
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    graph.AddNode(field.FieldType);
                    graph.AddEdge(type, field.FieldType);
                }

                // Add edges for method argument relationships
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                {
                    foreach (var parameter in method.GetParameters())
                    {
                        graph.AddNode(parameter.ParameterType);
                        graph.AddEdge(type, parameter.ParameterType);
                    }
                }

                // Add edges for generic type arguments
                if (type.IsGenericType)
                {
                    foreach (var argument in type.GetGenericArguments())
                    {
                        graph.AddNode(argument);
                        graph.AddEdge(type, argument);
                    }
                }
            }
        }

        return graph.ApplyConfiguration(configure);
    }
}
