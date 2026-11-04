#nullable enable

using System;

namespace Formbricks.Models;

public partial class OutboundEmailAudit
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime ContentExpiresAt { get; set; }

    public string Environment { get; set; } = null!;

    public string EmailType { get; set; } = null!;

    public string Trigger { get; set; } = null!;

    public string ActorKind { get; set; } = null!;

    public string? ActorUserId { get; set; }

    public string? ConfiguredByUserId { get; set; }

    public string? ActorUserHash { get; set; }

    public string? ConfiguredByUserHash { get; set; }

    public string? OrganizationId { get; set; }

    public string? SurveyId { get; set; }

    public string? ResponseId { get; set; }

    public string? FollowUpId { get; set; }

    public string? WorkflowId { get; set; }

    public string? WorkflowRunId { get; set; }

    public string? InviteId { get; set; }

    public string? RecipientMac { get; set; }

    public string? SesMessageHash { get; set; }

    public string? SnapshotKey { get; set; }

    public string? SnapshotSha256 { get; set; }

    public string Status { get; set; } = "prepared";

    public DateTime? StatusRecordedAt { get; set; }

    public bool AuditGap { get; set; } = false;

    public DateTime? ErasureRequestedAt { get; set; }

    public DateTime? ErasedAt { get; set; }

    public DateTime? ContentDeleteRetryAt { get; set; }

    public int ContentDeleteFailures { get; set; } = 0;
}
