#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraWorkflowSnapshot
{
    public string WorkflowName { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public string? ResourceId { get; set; }

    public string Snapshot { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
