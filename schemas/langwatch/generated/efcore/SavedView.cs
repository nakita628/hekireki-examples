#nullable enable

using System;

namespace LangWatch.Models;

public partial class SavedView
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string Name { get; set; } = null!;

    public string Filters { get; set; } = "{}";

    public string? Query { get; set; }

    public string? Period { get; set; }

    public int Order { get; set; } = 0;

    public string Kind { get; set; } = "v1-traces-filter";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
