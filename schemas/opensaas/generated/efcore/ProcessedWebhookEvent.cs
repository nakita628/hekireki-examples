#nullable enable

using System;

namespace OpenSaas.Models;

public partial class ProcessedWebhookEvent
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string PaymentProcessorEventId { get; set; } = null!;

    public string EventType { get; set; } = null!;
}
