﻿namespace FlowFi.Communication.Responses;

public class ResponseShortCategoryJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
