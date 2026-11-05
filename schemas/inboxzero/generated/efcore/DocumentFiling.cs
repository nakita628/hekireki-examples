#nullable enable

using System;

namespace InboxZero.Models;

public partial class DocumentFiling
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string MessageId { get; set; } = null!;

    public string AttachmentId { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string? FolderId { get; set; }

    public string FolderPath { get; set; } = null!;

    public string? FileId { get; set; }

    public string? Reasoning { get; set; }

    public double? Confidence { get; set; }

    public DocumentFilingStatus Status { get; set; } = DocumentFilingStatus.Filed;

    public bool WasAsked { get; set; } = false;

    public bool WasCorrected { get; set; } = false;

    public string? OriginalPath { get; set; }

    public DateTime? CorrectedAt { get; set; }

    public bool? FeedbackPositive { get; set; }

    public DateTime? FeedbackAt { get; set; }

    public string? NotificationMessageId { get; set; }

    public DateTime? NotificationSentAt { get; set; }

    public string? NotificationBatchId { get; set; }

    public string DriveConnectionId { get; set; } = null!;

    public virtual DriveConnection DriveConnection { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
