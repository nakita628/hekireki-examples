#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMPromptTable
{
    public string Id { get; set; } = null!;

    public string PromptId { get; set; } = null!;

    public int Version { get; set; } = 1;

    public string Environment { get; set; } = "development";

    public string? CreatedBy { get; set; }

    public string LitellmParams { get; set; } = null!;

    public string? PromptInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
