#nullable enable

using System;

namespace Calcom.Models;

public partial class WebhookScheduledTriggers
{
    public int Id { get; set; }

    public string? JobName { get; set; }

    public string SubscriberUrl { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public DateTime StartAfter { get; set; }

    public int RetryCount { get; set; } = 0;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public string? AppId { get; set; }

    public string? WebhookId { get; set; }

    public virtual Webhook? Webhook { get; set; }

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }
}
