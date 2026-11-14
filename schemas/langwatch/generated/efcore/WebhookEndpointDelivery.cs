#nullable enable

using System;

namespace LangWatch.Models;

public partial class WebhookEndpointDelivery
{
    public string Id { get; set; } = null!;

    public WebhookDeliveryChannel Channel { get; set; } = WebhookDeliveryChannel.Platform;

    public string? OrganizationId { get; set; }

    public string? EndpointId { get; set; }

    public virtual WebhookEndpoint? Endpoint { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string? TriggerId { get; set; }

    public virtual Trigger? Trigger { get; set; }

    public string DispatchId { get; set; } = null!;

    public int? Attempt { get; set; }

    public int? EventCount { get; set; }

    public int? ResponseStatus { get; set; }

    public int? LatencyMs { get; set; }

    public string? Error { get; set; }

    public string? Response { get; set; }

    public WebhookDeliveryOutcome Outcome { get; set; }

    public DateTime FiredAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
