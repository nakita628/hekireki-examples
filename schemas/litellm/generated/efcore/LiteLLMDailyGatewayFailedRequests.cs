#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyGatewayFailedRequests
{
    public string Date { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Route { get; set; } = null!;

    public int StatusCode { get; set; }

    public long FailedRequests { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
