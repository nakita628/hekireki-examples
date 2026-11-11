#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMDailyModelUsage
{
    public string Date { get; set; } = null!;

    public string ModelGroup { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string CustomLlmProvider { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public double Spend { get; set; } = 0.0;

    public long PromptTokens { get; set; } = 0L;

    public long CompletionTokens { get; set; } = 0L;

    public long RequestCount { get; set; } = 0L;

    public long SuccessfulRequests { get; set; } = 0L;

    public long FailedRequests { get; set; } = 0L;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
