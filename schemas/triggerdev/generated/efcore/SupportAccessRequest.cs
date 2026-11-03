#nullable enable

using System;

namespace Trigger.Models;

public partial class SupportAccessRequest
{
    public string Id { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual User? RequestedBy { get; set; }

    public string? RequestedById { get; set; }

    public string Reason { get; set; } = null!;

    public SupportAccessRequestStatus Status { get; set; } = SupportAccessRequestStatus.Pending;

    public virtual User? ApprovedBy { get; set; }

    public string? ApprovedById { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string? CancelledById { get; set; }

    public DateTime? CancelledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
