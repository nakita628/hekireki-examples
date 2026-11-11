#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyGlobalSpend
{
    public string Id { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string? Model { get; set; }

    public string? ModelGroup { get; set; }

    public string? CustomLlmProvider { get; set; }

    public string? McpNamespacedToolName { get; set; }

    public string? Endpoint { get; set; }

    public long PromptTokens { get; set; } = 0L;

    public long CompletionTokens { get; set; } = 0L;

    public long CacheReadInputTokens { get; set; } = 0L;

    public long CacheCreationInputTokens { get; set; } = 0L;

    public long CompressionSavedTokens { get; set; } = 0L;

    public double CompressionSavingsSpend { get; set; } = 0.0;

    public double PromptCachingSavingsSpend { get; set; } = 0.0;

    public double GatewayInjectedCachingSavingsSpend { get; set; } = 0.0;

    public double AutorouterSavingsSpend { get; set; } = 0.0;

    public double GuardrailSpend { get; set; } = 0.0;

    public double Spend { get; set; } = 0.0;

    public long ApiRequests { get; set; } = 0L;

    public long SuccessfulRequests { get; set; } = 0L;

    public long FailedRequests { get; set; } = 0L;

    public long TotalResponseTimeMs { get; set; } = 0L;

    public long TimedRequests { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
