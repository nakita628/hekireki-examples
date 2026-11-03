#nullable enable

using System;

namespace Documenso.Models;

public partial class WebhookCall
{
    public string Id { get; set; } = null!;

    public WebhookCallStatus Status { get; set; }

    public string Url { get; set; } = null!;

    public WebhookTriggerEvents Event { get; set; }

    public string RequestBody { get; set; } = null!;

    public int ResponseCode { get; set; }

    public string? ResponseHeaders { get; set; }

    public string? ResponseBody { get; set; }

    public DateTime CreatedAt { get; set; }

    public string WebhookId { get; set; } = null!;

    public virtual Webhook Webhook { get; set; } = null!;
}
