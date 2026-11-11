#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSpendLogGuardrailIndex
{
    public string RequestId { get; set; } = null!;

    public string GuardrailId { get; set; } = null!;

    public string? PolicyId { get; set; }

    public DateTime StartTime { get; set; }
}
