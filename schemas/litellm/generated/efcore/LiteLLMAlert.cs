#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMAlert
{
    public long Id { get; set; }

    public string DetectionKey { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string Status { get; set; } = "open";

    public DateTime WindowStart { get; set; }

    public DateTime WindowEnd { get; set; }

    public string Evidence { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
