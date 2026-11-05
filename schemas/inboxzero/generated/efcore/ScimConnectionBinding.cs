#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimConnectionBinding
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ConnectionKey { get; set; } = null!;

    public string ProvisioningDomainId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? DecommissionedAt { get; set; }

    public string DecommissionStatus { get; set; } = "active";

    public string? DecommissionCursorUserId { get; set; }

    public int DecommissionReconciledUserCount { get; set; } = 0;

    public int DecommissionBatchCount { get; set; } = 0;

    public int DecommissionRevision { get; set; } = 0;

    public DateTime? DecommissionCompletedAt { get; set; }

    public string? DecommissionLeaseId { get; set; }

    public DateTime? DecommissionLeaseExpiresAt { get; set; }
}
