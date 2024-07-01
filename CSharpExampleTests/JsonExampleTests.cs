using CSharpExample;

namespace CSharpExampleTests;

public class JsonExampleTests
{
    [Test]
    public void ExampleTest()
    {
        var payload = new Payload()
        {
            state = "state",
            status = "status",
            domain = "domain",
            event_id = "event_id",
            sub_domain = "sub_domain",
            event_body = new EventBody()
            {
                id = "id",
                status = "status",
                method = "method",
                message = "message",
            }
        };

        var str = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        Console.WriteLine(str);
    }
}
