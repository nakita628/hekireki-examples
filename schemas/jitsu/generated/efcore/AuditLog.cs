#nullable enable

using System;

namespace Jitsu.Models;

public partial class AuditLog
{
    public string Id { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Type { get; set; } = null!;

    public string? Severity { get; set; } = "info";

    public string? UserId { get; set; }

    public string? WorkspaceId { get; set; }

    public string? ObjectId { get; set; }

    public string? Changes { get; set; }

    public string? AuthType { get; set; }

    public string? TokenId { get; set; }

    public string? Ip { get; set; }

    public string? RequestHeaders { get; set; }
}
