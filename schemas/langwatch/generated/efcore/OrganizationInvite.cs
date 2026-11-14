#nullable enable

using System;

namespace LangWatch.Models;

public partial class OrganizationInvite
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string InviteCode { get; set; } = null!;

    public DateTime? Expiration { get; set; }

    public InviteStatus Status { get; set; } = InviteStatus.Pending;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string TeamIds { get; set; } = null!;

    public string? TeamAssignments { get; set; }

    public OrganizationUserRole Role { get; set; }

    public string? RequestedBy { get; set; }

    public virtual User? RequestedByUser { get; set; }

    public string? SubscriptionId { get; set; }

    public string? AcceptedByUserId { get; set; }

    public string? AcceptedViaIdentifierId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
