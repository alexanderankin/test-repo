using System.Diagnostics.CodeAnalysis;

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
    [SuppressMessage("ReSharper", "RedundantEmptyObjectCreationArgumentList")]
    [SuppressMessage("ReSharper", "ArrangeObjectCreationWhenTypeEvident")]
    public void TestWithInstances()
    {
        Example.Run(new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("i1", 100),
            new KeyValuePair<string, int>("i2", 100),
        });
    }

    [Test]
    public void TestWithInstances_SyntaxSugarSimplification()
    {
        Example.Run(new List<KeyValuePair<string, int>>
        {
            new("i1", 100),
            new("i2", 100),
        });
    }

    [Test]
    public void TestCorrectness()
    {
        var config = new List<KeyValuePair<string, int>>
        {
            new("i1", 50),
            new("i2", 50),
            new("i3", 100),
        };
        var results = new Dictionary<string, int>();
        for (int i = 0; i < 1000; i++)
        {
            var result = Example.Run(
                "[" +
                "{\"key\":\"i1\",\"value\": 50}," +
                "{\"key\":\"i2\",\"value\": 50}," +
                "{\"key\":\"i3\",\"value\": 100}" +
                "]");
            results[result] = (results.TryGetValue(result, out var oldValue) ? oldValue : 0) + 1;
        }

        Assert.That(results["i1"] / 1000.0, Is.EqualTo(0.25).Within(0.1));
        Assert.That(results["i2"] / 1000.0, Is.EqualTo(0.25).Within(0.1));
        Assert.That(results["i3"] / 1000.0, Is.EqualTo(0.5).Within(0.1));
    }
}