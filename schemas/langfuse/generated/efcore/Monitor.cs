#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Monitor
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User? UpdatedByUser { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public MonitorView View { get; set; }

    public string Filters { get; set; } = null!;

    public string Metric { get; set; } = null!;

    public long WindowMs { get; set; }

    public long CadenceMs { get; set; }

    public MonitorThresholdOperator ThresholdOperator { get; set; }

    public decimal AlertThreshold { get; set; }

    public decimal? WarningThreshold { get; set; }

    public MonitorSeverity Severity { get; set; } = MonitorSeverity.Unknown;

    public DateTime? SeverityChangedAt { get; set; }

    public string NoData { get; set; } = null!;

    public string Renotify { get; set; } = null!;

    public MonitorStatus Status { get; set; } = MonitorStatus.Active;

    public long SchedulerBatchId { get; set; }

    public DateTime? NextRunAt { get; set; }

    public DateTime? LastPublishedAt { get; set; }

    public DateTime? LastClaimedAt { get; set; }

    public DateTime? LastCompletedAt { get; set; }

    public string Name { get; set; } = null!;

    public List<string>? Tags { get; set; } = new List<string>();

    public List<string>? TriggerIds { get; set; } = new List<string>();

    public DateTime? AlertedAt { get; set; }
}
