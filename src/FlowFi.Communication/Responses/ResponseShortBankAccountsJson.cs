﻿namespace FlowFi.Communication.Responses;

public class ResponseShortBankAccountsJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}
