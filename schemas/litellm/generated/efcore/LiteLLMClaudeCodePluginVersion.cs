#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMClaudeCodePluginVersion
{
    public string Id { get; set; } = null!;

    public string PluginId { get; set; } = null!;

    public string? Version { get; set; }

    public string? Description { get; set; }

    public string? ManifestJson { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? PublishedBy { get; set; }

    public bool ArchiveAvailable { get; set; } = false;

    public byte[]? Content { get; set; }

    public virtual LiteLLMClaudeCodePluginTable Plugin { get; set; } = null!;
}
