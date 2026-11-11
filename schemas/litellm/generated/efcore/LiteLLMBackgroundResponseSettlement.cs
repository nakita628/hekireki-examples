#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMBackgroundResponseSettlement
{
    public string ResponseId { get; set; } = null!;

    public DateTime SettledAt { get; set; }
}
