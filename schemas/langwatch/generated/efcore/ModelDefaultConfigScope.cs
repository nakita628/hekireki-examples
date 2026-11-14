#nullable enable

using System;

namespace LangWatch.Models;

public partial class ModelDefaultConfigScope
{
    public string Id { get; set; } = null!;

    public string ConfigId { get; set; } = null!;

    public virtual ModelDefaultConfig Config { get; set; } = null!;

    public ModelDefaultScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
