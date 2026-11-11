#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMBackgroundInteractionSettlement
{
    public string InteractionId { get; set; } = null!;

    public string CustomLlmProvider { get; set; } = null!;

    public string CreateContext { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public string? ClaimedBy { get; set; }

    public DateTime? SettledAt { get; set; }

    public string? Outcome { get; set; }
}
