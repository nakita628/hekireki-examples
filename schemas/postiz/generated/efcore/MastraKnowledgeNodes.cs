#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraKnowledgeNodes
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string CanonicalName { get; set; } = null!;

    public string? Kind { get; set; }

    public string? Content { get; set; }

    public string? Description { get; set; }

    public string Scope { get; set; } = null!;

    public string ScopeKey { get; set; } = null!;

    public int Version { get; set; }

    public string? MergedInto { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
