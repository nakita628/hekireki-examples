#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Session
{
    public string Id { get; set; } = null!;

    public string SessionToken { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime Expires { get; set; }

    public virtual User User { get; set; } = null!;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? IdentifierId { get; set; }

    public List<string>? Amr { get; set; } = new List<string>();

    public bool StartedBySignUp { get; set; } = false;

    public string? Impersonating { get; set; }

    public string? ActorUserId { get; set; }

    public string? SubjectUserId { get; set; }

    public string? ImpersonationReason { get; set; }

    public DateTime? ImpersonationExpiresAt { get; set; }

    public DateTime? LastSeenAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
