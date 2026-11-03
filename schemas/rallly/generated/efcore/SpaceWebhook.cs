#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class SpaceWebhook
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public List<string>? Events { get; set; }

    public bool Enabled { get; set; } = true;

    public string Version { get; set; } = null!;

    public DateTime Cursor { get; set; }

    public DateTime? LastDeliveredAt { get; set; }

    public DateTime? LastFailedAt { get; set; }

    public int ConsecutiveFailures { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual ICollection<WebhookDelivery> Deliveries { get; set; } = new List<WebhookDelivery>();
}
