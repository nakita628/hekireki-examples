#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraChannelInstallations
{
    public string Id { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string AgentId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? WebhookId { get; set; }

    public string Data { get; set; } = null!;

    public string? ConfigHash { get; set; }

    public string? Error { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
