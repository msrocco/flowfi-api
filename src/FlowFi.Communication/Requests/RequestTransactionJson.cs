namespace FlowFi.Communication.Requests;

public class RequestTransactionJson
{
    public Guid BankAccountId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
