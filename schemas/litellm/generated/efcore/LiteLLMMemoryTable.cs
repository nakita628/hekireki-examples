#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMMemoryTable
{
    public string MemoryId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Metadata { get; set; }

    public string? UserId { get; set; }

    public string? TeamId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
