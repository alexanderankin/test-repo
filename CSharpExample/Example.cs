namespace CSharpExample;

public class Example
{
    public static string Run()
    {
        // data setup
        var instances = new List<KeyValuePair<string, int>>();

        // rendered data
        instances.Add(new KeyValuePair<string, int>("i1", 10));
        instances.Add(new KeyValuePair<string, int>("i2", 20));
        instances.Add(new KeyValuePair<string, int>("i3", 20));

        return Run(instances: instances);
    }

    public static string Run(string config)
    {
        /*
        var parsed = Newtonsoft.Json.Linq.JArray.Parse(config);
        var instances = new List<KeyValuePair<string, int>>();
        foreach (var token in parsed)
        {
            instances.Add(new KeyValuePair<string, int>(
                token.Value<string>("instance"),
                token.Value<int>("weight")
            ));
        }
        return Run(instances: instances);
        */

        var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(config);
        if (parsed == null) throw new Exception("unable to parse config");
        return Run(instances: parsed);
    }

    public static string Run(List<KeyValuePair<string, int>> instances)
    {
        // selecting a backend
        string backend = null;
        var total = instances.Sum(i => i.Value);

        var random = new Random().Next(0, total);

        var counter = 0;

        foreach (var (key, value) in instances)
        {
            counter += value;
            if (counter >= random)
            {
                backend = key;
                break;
            }
        }

        // ReSharper disable once ConvertIfStatementToNullCoalescingAssignment
        if (backend == null)
            backend = instances[^1].Key; // last

        Console.WriteLine($"random {random} gave backend {backend}");
        return backend;
    }
}