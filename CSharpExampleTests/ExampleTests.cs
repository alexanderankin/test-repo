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
        Example.Run(new Dictionary<string, List<KeyValuePair<string, int>>>
        {
            {
                "default", new List<KeyValuePair<string, int>>()
                {
                    new KeyValuePair<string, int>("i1", 100),
                    new KeyValuePair<string, int>("i2", 100),
                }
            }
        });
    }

    [Test]
    public void TestWithInstances_SyntaxSugarSimplification()
    {
        Example.Run(new Dictionary<string, List<KeyValuePair<string, int>>>
        {
            {
                "default", new List<KeyValuePair<string, int>>
                {
                    new("i1", 100),
                    new("i2", 100),
                }
            }
        });
    }

    [Test]
    public void TestCorrectness()
    {
        var results = new Dictionary<string, int>();
        for (int i = 0; i < 1000; i++)
        {
            var result = Example.RunDefault(
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

    [Test]
    public void TestCorrectnessWithMultipleClients()
    {
        var config = """
                     {
                       "client1": [
                         {"key": "i1", "value": 10},
                         {"key": "i2", "value": 10},
                         {"key": "i3", "value": 30},
                         {"key": "i4", "value": 50}
                       ],
                       "client2": [
                         {"key": "i1", "value": 50},
                         {"key": "i2", "value": 30},
                         {"key": "i3", "value": 10},
                         {"key": "i4", "value": 10},
                         {"key": "i5", "value": 100}
                       ]
                     }
                     """;
        var trials = 1000;
        var results = new Dictionary<string, Dictionary<string, int>>
        {
            { "client1", new Dictionary<string, int>() },
            { "client2", new Dictionary<string, int>() },
        };
        foreach (var client in results.Keys)
        {
            var clientResults = results[client];
            for (int i = 0; i < trials; i++)
            {
                var result = Example.Run(config, client);
                clientResults[result] = (clientResults.TryGetValue(result, out var oldValue) ? oldValue : 0) + 1;
            }
        }

        Assert.That(results["client1"]["i1"] / 1000.0, Is.EqualTo(0.1).Within(0.1));
        Assert.That(results["client1"]["i2"] / 1000.0, Is.EqualTo(0.1).Within(0.1));
        Assert.That(results["client1"]["i3"] / 1000.0, Is.EqualTo(0.3).Within(0.1));
        Assert.That(results["client1"]["i4"] / 1000.0, Is.EqualTo(0.5).Within(0.1));

        Assert.That(results["client2"]["i1"] / 1000.0, Is.EqualTo(0.5 / 2).Within(0.1));
        Assert.That(results["client2"]["i2"] / 1000.0, Is.EqualTo(0.3 / 2).Within(0.1));
        Assert.That(results["client2"]["i3"] / 1000.0, Is.EqualTo(0.1 / 2).Within(0.1));
        Assert.That(results["client2"]["i4"] / 1000.0, Is.EqualTo(0.1 / 2).Within(0.1));
        Assert.That(results["client2"]["i5"] / 1000.0, Is.EqualTo(0.5).Within(0.1));
    }
}