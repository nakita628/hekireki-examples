#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraKnowledgeCursors
{
    public string SourceThreadId { get; set; } = null!;

    public string Agent { get; set; } = null!;

    public string LastKnowledgeId { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
