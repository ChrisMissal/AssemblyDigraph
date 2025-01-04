namespace AssemblyGraphSolution.Tests;

public class SampleClass
{
    public void MethodWithPrimitive(int value) { }
    public void MethodWithCustomType(CustomType custom) { }
    public void MethodWithMultipleParams(int value, CustomType custom) { }
}
public class SampleClassWithCollections
{
    private List<int> _numbers;
    private Dictionary<string, int> _dictionary;

    public void AddItems(IEnumerable<string> items) { }
}