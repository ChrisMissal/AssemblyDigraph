using System.Reflection;
using AssemblyDigraph;
using FluentAssertions;

namespace AssemblyGraphSolution.Tests;

public class FieldGraphTests
{
    [Fact]
    public void ShouldIncludeFieldTypesAsNodes()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        graph.GetNodes().Should().NotContain(typeof(int), "because a private field has type int");
        graph.GetNodes().Should().Contain(typeof(CustomType), "because a public field has type CustomType");
    }

    [Fact]
    public void ShouldCreateEdgesForFieldRelationships()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        graph.GetEdges().Should().NotContain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(int),
            "because SampleClassWithFields has a private int field");
        graph.GetEdges().Should().Contain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(CustomType),
            "because SampleClassWithFields has a public CustomType field");
    }

    [Fact]
    public void ShouldRespectConfigurationForNonPublicFields()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph(config =>
        {
            //config.IncludeNonPublicTypes = false; // Exclude non-public types
        });

        // Assert
        graph.GetEdges().Should().NotContain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(int),
            "because non-public fields should be excluded by configuration");
    }
}
