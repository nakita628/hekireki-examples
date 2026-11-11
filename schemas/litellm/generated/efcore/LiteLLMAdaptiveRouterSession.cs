#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMAdaptiveRouterSession
{
    public string SessionId { get; set; } = null!;

    public string RouterName { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string ClassifiedType { get; set; } = null!;

    public int MisalignmentCount { get; set; } = 0;

    public int StagnationCount { get; set; } = 0;

    public int DisengagementCount { get; set; } = 0;

    public int SatisfactionCount { get; set; } = 0;

    public int FailureCount { get; set; } = 0;

    public int LoopCount { get; set; } = 0;

    public int ExhaustionCount { get; set; } = 0;

    public string? LastUserContent { get; set; }

    public string? LastAssistantContent { get; set; }

    public string ToolCallHistory { get; set; } = "[]";

    public string PendingToolCalls { get; set; } = "{}";

    public int TurnCount { get; set; } = 0;

    public int LastProcessedTurn { get; set; } = -1;

    public bool CleanCreditAwarded { get; set; } = false;

    public int? TerminalStatus { get; set; }

    public DateTime LastActivityAt { get; set; }
}
