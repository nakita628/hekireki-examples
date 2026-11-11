#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMProxyModelTable
{
    public string ModelId { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string LitellmParams { get; set; } = null!;

    public string? ModelInfo { get; set; }

    public bool Blocked { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
