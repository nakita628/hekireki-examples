#nullable enable

using System;

namespace Langfuse.Models;

public partial class TableViewPreset
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User? UpdatedByUser { get; set; }

    public string Filters { get; set; } = null!;

    public string ColumnOrder { get; set; } = null!;

    public string ColumnVisibility { get; set; } = null!;

    public string? SearchQuery { get; set; }

    public string? OrderBy { get; set; }
}
