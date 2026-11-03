#nullable enable

using System;

namespace Formbricks.Models;

public partial class AuthzedProjectionScopeState
{
    public string Scope { get; set; } = null!;

    public DateTime? ReadyAt { get; set; }

    public string? ReadyBy { get; set; }

    public DateTime UpdatedAt { get; set; }
}
