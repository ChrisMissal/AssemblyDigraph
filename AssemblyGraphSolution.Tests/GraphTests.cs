using System.Reflection;
using AssemblyDigraph;
using FluentAssertions;

namespace AssemblyGraphSolution.Tests;

public class GraphTests
{
    [Theory]
    [InlineData(typeof(int), false, "Primitive fields should not be included by default")]
    [InlineData(typeof(CustomType), true, "CustomType fields should be included by default")]
    public void ShouldIncludeDefaultFieldTypes(Type fieldType, bool isIncluded, string reason)
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        if (isIncluded)
        {
            graph.GetNodes().Should().Contain(fieldType, reason);
        }
        else
        {
            graph.GetNodes().Should().NotContain(fieldType, reason);
        }
    }

    [Fact]
    public void ShouldRespectExcludedNamespaces()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph(config =>
        {
            config.ExcludedNamespaces.Add("System");
        });

        // Assert
        graph.GetNodes().Should().NotContain(typeof(int), "because int belongs to the System namespace");
    }

    [Fact]
    public void ShouldIncludePrivateNonPrimitiveFieldsByDefault()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        graph.GetEdges().Should().Contain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(CustomType),
            "because private non-primitive fields should be included by default");
    }

    [Fact]
    public void ShouldExcludeNonPublicPrimitiveFieldsWhenConfigured()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        graph.GetEdges().Should().Contain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(CustomType),
            "because private fields should be excluded when IncludeNonPublicTypes is false");
        graph.GetEdges().Should().NotContain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(int),
            "because private fields should be excluded when IncludeNonPublicTypes is false");
    }

    [Fact]
    public void ShouldIncludePrimitiveFieldsWhenConfigured()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph(config =>
        {
            config.IncludePrimitiveTypes = true;
        });

        // Assert
        graph.GetEdges().Should().Contain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(int),
            "because primitive fields should be included when IncludePrimitiveTypes is true");
        graph.GetEdges().Should().Contain(edge => edge.From == typeof(SampleClassWithFields) && edge.To == typeof(CustomType),
            "because primitive fields should be included when IncludePrimitiveTypes is true");
    }

    [Fact]
    public void ShouldHandleMethodsWithNoArguments()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        graph.GetEdges().Should().NotContain(edge => edge.From == typeof(SampleClass) && edge.To == typeof(void),
            "because methods with no arguments should not create argument relationships");
    }

    [Theory]
    [InlineData(typeof(SampleClass), typeof(int), false, "MethodWithPrimitive should create an edge to int")]
    [InlineData(typeof(SampleClass), typeof(CustomType), true, "MethodWithCustomType should create an edge to CustomType")]
    public void ShouldCreateEdgesForMethodArguments(Type fromType, Type toType, bool edgeExists, string reason)
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };

        // Act
        var graph = assemblies.ToDigraph();

        // Assert
        if (edgeExists)
        {
            graph.GetEdges().Should().Contain(edge => edge.From == fromType && edge.To == toType, reason);
        }
        else
        {
            graph.GetEdges().Should().NotContain(edge => edge.From == fromType && edge.To == toType, reason);
        }
    }
}
