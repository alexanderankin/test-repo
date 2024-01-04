namespace CSharpExampleTests;

using CSharpExample;

public class ExampleTests
{
    [Test]
    public void Test1()
    {
        for (int i = 0; i < 100; i++)
        {
            Example.Run();
        }
    }

    [Test]
    public void TestWithInstances()
    {
        Example.Run(new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("i1", 100),
            new KeyValuePair<string, int>("i2", 100),
        });
    }
}