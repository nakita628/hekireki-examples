#nullable enable

using System;

namespace LangWatch.Models;

public partial class AuthzUserStanding
{
    public string UserId { get; set; } = null!;

    public DateTime? DeactivatedAt { get; set; }

    public DateTime? ErasedAt { get; set; }

    public DateTime StandingChangedAt { get; set; }
}
