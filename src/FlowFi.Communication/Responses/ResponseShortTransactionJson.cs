namespace FlowFi.Communication.Responses;

public class ResponseShortTransactionJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}
