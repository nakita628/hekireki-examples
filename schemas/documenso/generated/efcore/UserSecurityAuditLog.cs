#nullable enable

using System;

namespace Documenso.Models;

public partial class UserSecurityAuditLog
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public UserSecurityAuditLogType Type { get; set; }

    public string? UserAgent { get; set; }

    public string? IpAddress { get; set; }

    public virtual User User { get; set; } = null!;
}
