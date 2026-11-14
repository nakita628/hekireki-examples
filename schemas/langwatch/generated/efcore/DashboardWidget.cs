#nullable enable

using System;

namespace LangWatch.Models;

public partial class DashboardWidget
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? DashboardId { get; set; }

    public string Name { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public int GridColumn { get; set; } = 0;

    public int GridRow { get; set; } = 0;

    public int ColSpan { get; set; } = 1;

    public int RowSpan { get; set; } = 1;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
