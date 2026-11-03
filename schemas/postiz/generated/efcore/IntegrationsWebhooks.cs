#nullable enable

namespace Postiz.Models;

public partial class IntegrationsWebhooks
{
    public string IntegrationId { get; set; } = null!;

    public string WebhookId { get; set; } = null!;

    public virtual Integration Integration { get; set; } = null!;

    public virtual Webhooks Webhook { get; set; } = null!;
}
