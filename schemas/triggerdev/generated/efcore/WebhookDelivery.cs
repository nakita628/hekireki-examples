#nullable enable

using System;

namespace Trigger.Models;

public partial class WebhookDelivery
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string WebhookEndpointId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public RuntimeEnvironmentType EnvironmentType { get; set; }

    public string ExternalDeliveryId { get; set; } = null!;

    public string IdempotencyKey { get; set; } = null!;

    public string? RunId { get; set; }

    public WebhookDeliveryStatus Status { get; set; } = WebhookDeliveryStatus.Pending;

    public bool IsTest { get; set; } = false;

    public string? ParsedEvent { get; set; }

    public string? Headers { get; set; }

    public string? RawBodyHash { get; set; }

    public string? ErrorMessage { get; set; }

    public string? FilterReason { get; set; }

    public string Targets { get; set; } = "[]";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public int RetentionDays { get; set; } = 30;
}
