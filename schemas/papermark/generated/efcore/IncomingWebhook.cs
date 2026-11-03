#nullable enable

using System;

namespace Papermark.Models;

public partial class IncomingWebhook
{
    public string Id { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Secret { get; set; }

    public string? Source { get; set; }

    public string? Actions { get; set; }

    public int ConsecutiveFailures { get; set; } = 0;

    public DateTime? LastFailedAt { get; set; }

    public DateTime? DisabledAt { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
