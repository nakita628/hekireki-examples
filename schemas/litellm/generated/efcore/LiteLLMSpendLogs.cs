#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSpendLogs
{
    public string RequestId { get; set; } = null!;

    public string CallType { get; set; } = null!;

    public string ApiKey { get; set; } = "";

    public double Spend { get; set; } = 0.0;

    public int TotalTokens { get; set; } = 0;

    public int PromptTokens { get; set; } = 0;

    public int CompletionTokens { get; set; } = 0;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int? RequestDurationMs { get; set; }

    public DateTime? CompletionStartTime { get; set; }

    public string Model { get; set; } = "";

    public string? ModelId { get; set; } = "";

    public string? ModelGroup { get; set; } = "";

    public string? CustomLlmProvider { get; set; } = "";

    public string? ApiBase { get; set; } = "";

    public string? User { get; set; } = "";

    public string? Metadata { get; set; } = "{}";

    public string? CacheHit { get; set; } = "";

    public string? CacheKey { get; set; } = "";

    public string? RequestTags { get; set; } = "[]";

    public string? TeamId { get; set; }

    public string? OrganizationId { get; set; }

    public string? EndUser { get; set; }

    public string? RequesterIpAddress { get; set; }

    public string? Messages { get; set; } = "{}";

    public string? Response { get; set; } = "{}";

    public string? SessionId { get; set; }

    public string? Status { get; set; }

    public string? McpNamespacedToolName { get; set; }

    public string? BillingAgentId { get; set; }

    public string? AgentId { get; set; }

    public string? ProxyServerRequest { get; set; } = "{}";

    public string? LitellmCallId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
