#nullable enable

using System;

namespace WorkoutCool.Models;

public partial class RevenueCatWebhookEvent
{
    public string Id { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public DateTime EventTimestamp { get; set; }

    public string AppUserId { get; set; } = null!;

    public string Environment { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? TransactionId { get; set; }

    public string? OriginalTransactionId { get; set; }

    public string? EntitlementIds { get; set; }

    public bool Processed { get; set; } = false;

    public string? ProcessingError { get; set; }

    public int RetryCount { get; set; } = 0;

    public string RawEventData { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
