#nullable enable

using System;

namespace Rallly.Models;

public partial class WebhookDelivery
{
    public string Id { get; set; } = null!;

    public string WebhookId { get; set; } = null!;

    public string ActivityId { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public WebhookDeliveryStatus Status { get; set; } = WebhookDeliveryStatus.Pending;

    public int Attempts { get; set; } = 0;

    public DateTime NextAttemptAt { get; set; }

    public int? LastResponseStatus { get; set; }

    public string? LastError { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual SpaceWebhook Webhook { get; set; } = null!;
}
