// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using Newtonsoft.Json.Linq;

static void Program()
{
    IDictionary<string, int> a = new Dictionary<string, int>();

    a.Add("abc", 1);

    foreach (var i in a)
    {
        Console.WriteLine(i.Key + ": " + i.Value);
    }

    int total = 10;
    new Random().Next(0, total);
}

static object SomeMethod()
{
    IDictionary<string, int> instanceCapacityDict = new Dictionary<string, int>();
    instanceCapacityDict.Add("oai01", 30);
    instanceCapacityDict.Add("oai02", 20);
    instanceCapacityDict.Add("oai03", 10);
    instanceCapacityDict.Add("oai04", 40);

    var total = instanceCapacityDict.Sum(x => x.Value);
    var random = new Random().Next(0, total);
    IDictionary<string, int> instanceDistributionDict = new Dictionary<string, int>();
    int cumul = 0;
    foreach (var i in instanceCapacityDict)
    {
        cumul += i.Value;
        instanceDistributionDict.Add(i.Key, cumul);
    }

    foreach (var i in instanceDistributionDict)
    {
        if (i.Value > random) return i.Key;
    }

    return null;
}

Dictionary<string, Dictionary<string, Dictionary<string, string>>> item =
    new Dictionary<string, Dictionary<string, Dictionary<string, string>>>
    {
        {
            "outer.1", new Dictionary<string, Dictionary<string, string>>
            {
                { "outer-2", new Dictionary<string, string> { { "inner", "value" } } }
            }
        }
    };

var jObjectFromObject = JObject.FromObject(item);
var jObjectFromString =
    JObject.Parse(
        "{\"outer.1\":{\"outer-2\": {\"inner\":\"value\"}}}"
    );

foreach (var jObject in new List<JObject> { jObjectFromObject, jObjectFromString })
{
    // Console.WriteLine(item.GetValueOrDefault("outer.1", null)?.GetValueOrDefault("outer-2-DNE", null)?.GetValueOrDefault("inner", null));
    // Console.WriteLine(item["outer.1"]["outer-2-DNE"]?["inner"]);

    Console.WriteLine("works: " + jObject.SelectToken("$.['outer.1'].['outer-2'].['inner']")?.ToString());
    Console.WriteLine("works: " +
                      jObject.Value<JObject>("outer.1")?.Value<JObject>("outer-2")?.GetValue("inner")?.ToString());
    Console.WriteLine("not working: " +
                      jObject.Value<JObject>("outer.1")?.Value<JObject>("outer.DNE")?.GetValue("inner")?.ToString());
}
