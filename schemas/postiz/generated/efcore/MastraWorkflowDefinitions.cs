#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraWorkflowDefinitions
{
    public string Id { get; set; } = null!;

    public string? Description { get; set; }

    public string? Metadata { get; set; }

    public string InputSchema { get; set; } = null!;

    public string OutputSchema { get; set; } = null!;

    public string? StateSchema { get; set; }

    public string? RequestContextSchema { get; set; }

    public string Graph { get; set; } = null!;

    public string? Schedule { get; set; }

    public string Status { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string? AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
