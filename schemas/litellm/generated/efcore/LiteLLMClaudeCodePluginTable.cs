#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMClaudeCodePluginTable
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Version { get; set; }

    public string? Description { get; set; }

    public string? ManifestJson { get; set; }

    public string? FilesJson { get; set; } = "{}";

    public bool Enabled { get; set; } = true;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? CreatedBy { get; set; }

    public virtual LiteLLMClaudeCodePluginUpload? UploadedArchive { get; set; }

    public virtual ICollection<LiteLLMClaudeCodePluginVersion> Versions { get; set; } = new List<LiteLLMClaudeCodePluginVersion>();

    public virtual ICollection<LiteLLMClaudeCodePluginDownload> Downloads { get; set; } = new List<LiteLLMClaudeCodePluginDownload>();

    public long AnonymousDownloadCount { get; set; } = 0L;
}
