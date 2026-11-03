#nullable enable

using System;

namespace Trigger.Models;

public partial class ImpersonationAuditLog
{
    public string Id { get; set; } = null!;

    public ImpersonationAuditLogAction Action { get; set; }

    public virtual User Admin { get; set; } = null!;

    public string AdminId { get; set; } = null!;

    public virtual User Target { get; set; } = null!;

    public string TargetId { get; set; } = null!;

    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; }
}
