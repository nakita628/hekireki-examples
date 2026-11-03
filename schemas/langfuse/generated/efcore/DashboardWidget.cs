#nullable enable

using System;

namespace Langfuse.Models;

public partial class DashboardWidget
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User? UpdatedByUser { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DashboardWidgetViews View { get; set; }

    public string Dimensions { get; set; } = null!;

    public string Metrics { get; set; } = null!;

    public string Filters { get; set; } = null!;

    public DashboardWidgetChartType ChartType { get; set; }

    public string ChartConfig { get; set; } = null!;

    public int MinVersion { get; set; } = 1;
}
