#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraKnowledgeRecords
{
    public string Id { get; set; } = null!;

    public string Node { get; set; } = null!;

    public string Text { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string ScopeKey { get; set; } = null!;

    public string SourceThreadId { get; set; } = null!;

    public DateTime CapturedAt { get; set; }

    public DateTime? When { get; set; }

    public string? MaxScope { get; set; }

    public string? Metadata { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? CapturedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? WhenZ { get; set; } = DateTime.UtcNow;

    public DateTime? DeletedAtZ { get; set; } = DateTime.UtcNow;
}
