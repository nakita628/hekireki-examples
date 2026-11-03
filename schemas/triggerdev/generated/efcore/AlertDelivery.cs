#nullable enable

using System;

namespace Trigger.Models;

public partial class AlertDelivery
{
    public string Id { get; set; } = null!;

    public string AlertEventId { get; set; } = null!;

    public DateTime EventAt { get; set; }

    public string NotifierId { get; set; } = null!;

    public string Notifier { get; set; } = null!;

    public string Status { get; set; } = "pending";

    public string? ClaimId { get; set; }

    public DateTime? ClaimedAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public string? FailureReason { get; set; }

    public string? ProviderMetadata { get; set; }

    public int Attempts { get; set; } = 0;

    public DateTime? FirstAttemptAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
