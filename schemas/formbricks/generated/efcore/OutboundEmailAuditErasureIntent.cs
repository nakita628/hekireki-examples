#nullable enable

using System;

namespace Formbricks.Models;

public partial class OutboundEmailAuditErasureIntent
{
    public string Id { get; set; } = null!;

    public string? UserId { get; set; }

    public string UserHash { get; set; } = null!;

    public string RecipientMac { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ActivatedAt { get; set; }

    public DateTime? CheckedAt { get; set; }

    public DateTime ExpiresAt { get; set; }
}
