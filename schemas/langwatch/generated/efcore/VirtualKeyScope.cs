#nullable enable

using System;

namespace LangWatch.Models;

public partial class VirtualKeyScope
{
    public string Id { get; set; } = null!;

    public string VirtualKeyId { get; set; } = null!;

    public virtual VirtualKey VirtualKey { get; set; } = null!;

    public VirtualKeyScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
