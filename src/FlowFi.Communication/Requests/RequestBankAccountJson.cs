namespace FlowFi.Communication.Requests;

public class RequestBankAccountJson
{
    public string Name { get; set; } = String.Empty;
    public decimal InitialBalance { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
