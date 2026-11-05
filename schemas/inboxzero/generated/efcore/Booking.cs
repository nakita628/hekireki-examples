#nullable enable

using System;

namespace InboxZero.Models;

public partial class Booking
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string GuestName { get; set; } = null!;

    public string GuestEmail { get; set; } = null!;

    public string? GuestNote { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.PendingProviderEvent;

    public string? Provider { get; set; }

    public string? ProviderConnectionId { get; set; }

    public string? ProviderCalendarId { get; set; }

    public string? ProviderEventId { get; set; }

    public string? VideoConferenceLink { get; set; }

    public string CancelTokenHash { get; set; } = null!;

    public string? CancellationReason { get; set; }

    public string? IdempotencyToken { get; set; }

    public string BookingLinkId { get; set; } = null!;

    public virtual BookingLink BookingLink { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
