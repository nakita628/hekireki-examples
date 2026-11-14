#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class WebhookEndpoint
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public WebhookDestinationKind DestinationKind { get; set; } = WebhookDestinationKind.Http;

    public string? Url { get; set; }

    public string? SqsQueueUrl { get; set; }

    public string? SqsRoleArn { get; set; }

    public string? SqsExternalId { get; set; }

    public string? SqsAccessKeyId { get; set; }

    public string? SqsSecretAccessKeyEncrypted { get; set; }

    public string SecretEncrypted { get; set; } = null!;

    public string? SignatureScheme { get; set; }

    public string? IdempotencyKey { get; set; }

    public string? PreviousSecretEncrypted { get; set; }

    public DateTime? PreviousSecretExpiresAt { get; set; }

    public List<string>? EnabledEvents { get; set; } = new List<string>();

    public WebhookEndpointStatus Status { get; set; } = WebhookEndpointStatus.Active;

    public string? DisabledReason { get; set; }

    public DateTime? DisabledAt { get; set; }

    public DateTime? FailingSince { get; set; }

    public int MaxBatchSize { get; set; } = 100;

    public int MaxBatchDelayMs { get; set; } = 250;

    public int MaxInFlight { get; set; } = 4;

    public bool AllowSelfSignedCertificate { get; set; } = false;

    public DateTime? LastSuccessAt { get; set; }

    public DateTime? LastFailureAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<WebhookEndpointDelivery> Deliveries { get; set; } = new List<WebhookEndpointDelivery>();
}
