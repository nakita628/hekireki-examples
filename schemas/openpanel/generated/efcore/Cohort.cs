#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Cohort
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Definition { get; set; } = "{}";

    public bool IsStatic { get; set; } = false;

    public int ProfileCount { get; set; } = 0;

    public DateTime? LastComputedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
