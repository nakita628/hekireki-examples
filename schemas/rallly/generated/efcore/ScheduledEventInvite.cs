#nullable enable

using System;

namespace Rallly.Models;

public partial class ScheduledEventInvite
{
    public string Id { get; set; } = null!;

    public string Uid { get; set; } = null!;

    public string ScheduledEventId { get; set; } = null!;

    public string InviteeName { get; set; } = null!;

    public string InviteeEmail { get; set; } = null!;

    public string? InviteeId { get; set; }

    public string? InviteeTimeZone { get; set; }

    public string? InviteeLocale { get; set; }

    public ScheduledEventInviteStatus Status { get; set; } = ScheduledEventInviteStatus.Pending;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ScheduledEvent ScheduledEvent { get; set; } = null!;

    public virtual User? User { get; set; }
}
