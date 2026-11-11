#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMToolTable
{
    public string ToolId { get; set; } = null!;

    public string ToolName { get; set; } = null!;

    public string? Origin { get; set; }

    public string InputPolicy { get; set; } = "untrusted";

    public string OutputPolicy { get; set; } = "untrusted";

    public int CallCount { get; set; } = 0;

    public string? Assignments { get; set; } = "{}";

    public string? KeyHash { get; set; }

    public string? TeamId { get; set; }

    public string? KeyAlias { get; set; }

    public string? UserAgent { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
