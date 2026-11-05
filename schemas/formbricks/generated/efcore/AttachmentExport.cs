#nullable enable

using System;

namespace Formbricks.Models;

public partial class AttachmentExport
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual User? RequestedBy { get; set; }

    public string? RequestedById { get; set; }

    public AttachmentExportStatus Status { get; set; } = AttachmentExportStatus.Queued;

    public string? FilterCriteria { get; set; }

    public string TimeZone { get; set; } = null!;

    public int DispatchCount { get; set; } = 0;

    public int FileCount { get; set; }

    public int ResponseCount { get; set; }

    public int? IncludedFileCount { get; set; }

    public long? TotalBytes { get; set; }

    public bool Truncated { get; set; } = false;

    public string? StorageKey { get; set; }

    public string? ErrorCode { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
