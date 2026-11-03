#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class Webhook
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? InstallationId { get; set; }

    public WebhookReceiver Receiver { get; set; } = WebhookReceiver.User;

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public string Triggers { get; set; } = null!;

    public LinkScope? LinkScope { get; set; }

    public int ConsecutiveFailures { get; set; } = 0;

    public DateTime? LastFailedAt { get; set; }

    public DateTime? DisabledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual InstalledIntegration? InstalledIntegration { get; set; }

    public virtual ICollection<LinkWebhook> Links { get; set; } = new List<LinkWebhook>();

    public virtual ICollection<FolderWebhook> Folders { get; set; } = new List<FolderWebhook>();
}
