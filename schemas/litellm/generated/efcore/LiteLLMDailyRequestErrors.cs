#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyRequestErrors
{
    public string Date { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public string TeamId { get; set; } = "";

    public string UserId { get; set; } = "";

    public string ModelGroup { get; set; } = "";

    public int StatusCode { get; set; }

    public long FailedRequests { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
