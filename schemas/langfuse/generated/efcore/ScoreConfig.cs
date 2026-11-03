#nullable enable

using System;

namespace Langfuse.Models;

public partial class ScoreConfig
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public ScoreConfigDataType DataType { get; set; }

    public bool IsArchived { get; set; } = false;

    public double? MinValue { get; set; }

    public double? MaxValue { get; set; }

    public string? Categories { get; set; }

    public string? Description { get; set; }
}
