#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraKnowledgeActivity
{
    public string Id { get; set; } = null!;

    public string Action { get; set; } = null!;

    public string RecordType { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string ScopeKey { get; set; } = null!;

    public string? SourceThreadId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
