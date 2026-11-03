#nullable enable

using System;

namespace Langfuse.Models;

public partial class DatasetRuns
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Metadata { get; set; }

    public string DatasetId { get; set; } = null!;

    public virtual Dataset Dataset { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
