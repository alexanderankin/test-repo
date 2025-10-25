using System;
using System.IO;
using System.Threading;

string path = Path.Combine(Environment.CurrentDirectory, "output.txt");
Console.WriteLine($"Child running. Logging to {path}");

while (true)
{
    string line = $"hello {DateTime.Now:O}";
    File.AppendAllText(path, line + Environment.NewLine);
    Thread.Sleep(1000);
}