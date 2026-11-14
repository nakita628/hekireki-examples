#nullable enable

using System;

namespace LangWatch.Models;

public partial class ModelProviderScope
{
    public string Id { get; set; } = null!;

    public string ModelProviderId { get; set; } = null!;

    public virtual ModelProvider ModelProvider { get; set; } = null!;

    public ModelProviderScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
