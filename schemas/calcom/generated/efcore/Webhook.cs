#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Webhook
{
    public string Id { get; set; } = null!;

    public int? UserId { get; set; }

    public int? TeamId { get; set; }

    public int? EventTypeId { get; set; }

    public string? PlatformOAuthClientId { get; set; }

    public string SubscriberUrl { get; set; } = null!;

    public string? PayloadTemplate { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Active { get; set; } = true;

    public List<WebhookTriggerEvents>? EventTriggers { get; set; }

    public virtual User? User { get; set; }

    public virtual Team? Team { get; set; }

    public virtual EventType? EventType { get; set; }

    public virtual PlatformOAuthClient? PlatformOAuthClient { get; set; }

    public virtual App? App { get; set; }

    public string? AppId { get; set; }

    public string? Secret { get; set; }

    public bool Platform { get; set; } = false;

    public virtual ICollection<WebhookScheduledTriggers> ScheduledTriggers { get; set; } = new List<WebhookScheduledTriggers>();

    public int? Time { get; set; }

    public TimeUnit? TimeUnit { get; set; }

    public string Version { get; set; } = "2021-10-20";
}
