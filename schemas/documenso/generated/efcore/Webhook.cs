#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Webhook
{
    public string Id { get; set; } = null!;

    public string WebhookUrl { get; set; } = null!;

    public List<WebhookTriggerEvents>? EventTriggers { get; set; }

    public string? Secret { get; set; }

    public bool Enabled { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<WebhookCall> WebhookCalls { get; set; } = new List<WebhookCall>();
}
