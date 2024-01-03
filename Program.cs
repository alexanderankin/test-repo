// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

IDictionary<string, int> a = new Dictionary<string, int>();

a.Add("abc", 1);

foreach (var i in a)
{
    Console.WriteLine(i.Key + ": " + i.Value);
}

int total = 10;
new Random().Next(0, total);
