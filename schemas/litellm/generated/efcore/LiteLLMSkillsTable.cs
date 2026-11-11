#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSkillsTable
{
    public string SkillId { get; set; } = null!;

    public string? DisplayTitle { get; set; }

    public string? Description { get; set; }

    public string? Instructions { get; set; }

    public string Source { get; set; } = "custom";

    public string? LatestVersion { get; set; }

    public byte[]? FileContent { get; set; }

    public string? FileName { get; set; }

    public string? FileType { get; set; }

    public string? Metadata { get; set; } = "{}";

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
