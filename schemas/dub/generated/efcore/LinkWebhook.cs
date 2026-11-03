#nullable enable

using System;

namespace Dub.Models;

public partial class LinkWebhook
{
    public string Id { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public string WebhookId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Link Link { get; set; } = null!;

    public virtual Webhook Webhook { get; set; } = null!;
}
