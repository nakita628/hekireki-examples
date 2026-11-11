#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMClaudeCodePluginDownload
{
    public long Id { get; set; }

    public string PluginId { get; set; } = null!;

    public DateTime DownloadedAt { get; set; }

    public string? Version { get; set; }

    public string Digest { get; set; } = null!;

    public bool Authenticated { get; set; }

    public string? UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? KeyAlias { get; set; }

    public string? TeamId { get; set; }

    public string? TeamAlias { get; set; }

    public virtual LiteLLMClaudeCodePluginTable Plugin { get; set; } = null!;
}
