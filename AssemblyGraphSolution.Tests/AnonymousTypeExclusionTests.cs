using AssemblyDigraph;
using FluentAssertions;
using System.Reflection;

namespace AssemblyGraphSolution.Tests;

public class AnonymousTypeExclusionTests
{
    [Fact]
    public void ShouldExcludeAnonymousAndCompilerGeneratedTypes()
    {
        // Arrange
        var assemblies = new[] { Assembly.GetExecutingAssembly() };
        var graph = assemblies.ToDigraph();

        // Act
        var containsAnonymousType = graph.GetNodes().Any(node => node.Name.Contains("<"));

        // Assert
        containsAnonymousType.Should().BeFalse("because anonymous and compiler-generated types should be excluded");
    }
}
