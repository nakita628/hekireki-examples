#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMSkillSubmission
{
    public string Id { get; set; } = null!;

    public string PluginName { get; set; } = null!;

    public string Operation { get; set; } = null!;

    public string Status { get; set; } = "pending_review";

    public string ProposalJson { get; set; } = null!;

    public byte[]? ArchiveContent { get; set; }

    public string SubmittedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? BasePluginId { get; set; }

    public DateTime? BaseUpdatedAt { get; set; }

    public string? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? RejectionReason { get; set; }

    public string? ScanJson { get; set; }
}
