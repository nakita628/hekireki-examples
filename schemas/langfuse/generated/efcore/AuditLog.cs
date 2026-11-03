#nullable enable

using System;

namespace Langfuse.Models;

public partial class AuditLog
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public AuditLogRecordType Type { get; set; } = AuditLogRecordType.User;

    public string? ApiKeyId { get; set; }

    public string? UserId { get; set; }

    public string OrgId { get; set; } = null!;

    public string? UserOrgRole { get; set; }

    public string? ProjectId { get; set; }

    public string? UserProjectRole { get; set; }

    public string ResourceType { get; set; } = null!;

    public string ResourceId { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string? Before { get; set; }

    public string? After { get; set; }
}
