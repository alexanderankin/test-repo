namespace CSharpExampleTests;

public class DictionaryFilterTests
{
    public Dictionary<string, string> Filter(IReadOnlyDictionary<string, string> dictionary)
    {
        return dictionary
            .Where(pair =>
            {
                var lower = pair.Key.ToLower();
                return !lower.Contains("auth") && !lower.Contains("key");
            })
            .ToDictionary(i => i.Key, i => i.Value);
    }
    
    [Test]
    public void test_dictionaryFilter()
    {
        
        IReadOnlyDictionary<string, string> input =
            new Dictionary<string, string>
            {
                // good
                { "hello", "world" },
                // bad, both upper and lower
                { "key", "key1" },
                { "KEY", "key1" },
                { "KEy", "key1" },
                // good
                { "Automobile", "honda" },
                // bad - contains "auth"
                { "Author", "cs lewis" },
                { "Authorization", "bearer token" },
                { "X-Authentication", "some-contents" },
            }.AsReadOnly();

        var expected = new Dictionary<string, string>
        {
                { "hello", "world" },
                { "Automobile", "honda" },
        };
        
        var result = Filter(input);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}