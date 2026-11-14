#nullable enable

using System;

namespace LangWatch.Models;

public partial class IdentitySignInSwitch
{
    public string UserId { get; set; } = null!;

    public bool OnIdentifiers { get; set; }

    public bool Pinned { get; set; } = false;

    public DateTime SwitchedAt { get; set; }

    public string? LastAppliedEventId { get; set; }

    public DateTime UpdatedAt { get; set; }
}
