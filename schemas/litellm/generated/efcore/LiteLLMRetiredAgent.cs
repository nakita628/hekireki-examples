#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMRetiredAgent
{
    public string OriginalAgentId { get; set; } = null!;

    public DateTime RetiredAt { get; set; }
}
