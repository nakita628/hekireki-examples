#nullable enable

using System;

namespace LangWatch.Models;

public partial class AuditLog
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? UserId { get; set; }

    public string? ProjectId { get; set; }

    public string? OrganizationId { get; set; }

    public string Action { get; set; } = null!;

    public string? Args { get; set; }

    public string? Error { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? Metadata { get; set; }

    public string? ActorUserId { get; set; }

    public string? TargetKind { get; set; }

    public string? TargetId { get; set; }

    public string? Before { get; set; }

    public string? After { get; set; }

    public string? IdempotencyKey { get; set; }
}
