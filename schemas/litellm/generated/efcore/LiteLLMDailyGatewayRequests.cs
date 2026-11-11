#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyGatewayRequests
{
    public string Date { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Route { get; set; } = null!;

    public long SuccessfulRequests { get; set; } = 0L;

    public long FailedRequests { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
