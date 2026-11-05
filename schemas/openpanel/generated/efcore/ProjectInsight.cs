#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class ProjectInsight
{
    public Guid Id { get; set; }

    public string ProjectId { get; set; } = null!;

    public string ModuleKey { get; set; } = null!;

    public string DimensionKey { get; set; } = null!;

    public string WindowKind { get; set; } = null!;

    public InsightState State { get; set; } = InsightState.Active;

    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public string DisplayName { get; set; } = "";

    public string Payload { get; set; } = "{}";

    public string? Direction { get; set; }

    public double ImpactScore { get; set; } = 0.0;

    public string? SeverityBand { get; set; }

    public double? RelevanceScore { get; set; }

    public string? AiSummary { get; set; }

    public string? AiCategory { get; set; }

    public bool EmailWorthy { get; set; } = false;

    public bool ReferenceWorthy { get; set; } = false;

    public DateTime? EnrichedAt { get; set; }

    public int? EnrichVersion { get; set; }

    public int Version { get; set; } = 1;

    public Guid ThreadId { get; set; }

    public DateTime? WindowStart { get; set; }

    public DateTime? WindowEnd { get; set; }

    public DateTime FirstDetectedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    public DateTime LastSeenAt { get; set; }

    public virtual ICollection<InsightEvent> Events { get; set; } = new List<InsightEvent>();
}
