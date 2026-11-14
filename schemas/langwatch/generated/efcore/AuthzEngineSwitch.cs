#nullable enable

using System;

namespace LangWatch.Models;

public partial class AuthzEngineSwitch
{
    public string OrganizationId { get; set; } = null!;

    public bool OnEngine { get; set; }

    public bool Pinned { get; set; } = false;

    public DateTime SwitchedAt { get; set; }

    public string? LastAppliedEventId { get; set; }

    public DateTime UpdatedAt { get; set; }
}
