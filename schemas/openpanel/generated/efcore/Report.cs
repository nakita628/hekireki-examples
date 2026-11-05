#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class Report
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Interval Interval { get; set; }

    public string Range { get; set; } = "30d";

    public ChartType ChartType { get; set; }

    public string LineType { get; set; } = "monotone";

    public string Breakdowns { get; set; } = null!;

    public string Events { get; set; } = null!;

    public string GlobalFilters { get; set; } = "[]";

    public string? Formula { get; set; }

    public string? Unit { get; set; }

    public Metric Metric { get; set; } = Metric.Sum;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public bool Previous { get; set; } = false;

    public string? Criteria { get; set; }

    public string? FunnelGroup { get; set; }

    public double? FunnelWindow { get; set; }

    public string? Options { get; set; }

    public string? Display { get; set; }

    public List<string>? VisibleSeries { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public string DashboardId { get; set; } = null!;

    public virtual Dashboard Dashboard { get; set; } = null!;

    public virtual ReportLayout? Layout { get; set; }

    public virtual ShareReport? Share { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
