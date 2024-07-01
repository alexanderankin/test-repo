namespace CSharpExample;

public class JsonExample
{
}

public class EventBody
{
    public string id { get; set; }
    public string status { get; set; }
    public string method { get; set; }
    public object message { get; set; }
}

public class Payload
{
    public string state { get; set; }
    public string status { get; set; }
    public string domain { get; set; }
    public string event_id { get; set; }
    public string sub_domain { get; set; }
    public EventBody event_body { get; set; }
}
