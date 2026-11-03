#nullable enable

using System;

namespace Dub.Models;

public partial class FolderWebhook
{
    public string Id { get; set; } = null!;

    public string FolderId { get; set; } = null!;

    public string WebhookId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Folder Folder { get; set; } = null!;

    public virtual Webhook Webhook { get; set; } = null!;
}
