// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
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