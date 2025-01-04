using GenericDirectedGraph;
using FluentAssertions;

namespace AssemblyGraphSolution.Tests;

public class DirectedGraphTests
{
    [Fact]
    public void ContainsNode_ShouldReturnTrueForExistingNode()
    {
        // Arrange
        var graph = new DirectedGraph<string>();
        graph.AddNode("A");

        // Act
        var result = graph.ContainsNode("A");

        // Assert
        result.Should().BeTrue("because the node 'A' was added to the graph");
    }

    [Fact]
    public void ContainsNode_ShouldReturnFalseForNonExistingNode()
    {
        // Arrange
        var graph = new DirectedGraph<string>();

        // Act
        var result = graph.ContainsNode("A");

        // Assert
        result.Should().BeFalse("because the node 'A' was not added to the graph");
    }
}
