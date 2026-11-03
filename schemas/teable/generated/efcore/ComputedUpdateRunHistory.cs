#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class ComputedUpdateRunHistory
{
    public string TaskId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string SeedTableId { get; set; } = null!;

    public string ChangeType { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public List<string>? OriginRunIds { get; set; } = new List<string>();

    public string? Steps { get; set; }

    public string? Edges { get; set; }

    public List<string>? AffectedTableIds { get; set; } = new List<string>();

    public List<string>? AffectedFieldIds { get; set; } = new List<string>();

    public List<string>? SourceFieldIds { get; set; } = new List<string>();

    public int SeedRecordCount { get; set; } = 0;

    public int StageDepth { get; set; } = 0;

    public string? PredecessorTaskId { get; set; }

    public int RunTotalSteps { get; set; } = 0;

    public int RunCompletedStepsBefore { get; set; } = 0;

    public int? SyncMaxLevel { get; set; }

    public int EstimatedComplexity { get; set; } = 0;

    public int Attempts { get; set; } = 0;

    public string Outcome { get; set; } = null!;

    public DateTime? SourceChangedAt { get; set; }

    public DateTime EnqueuedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime CompletedAt { get; set; }

    public int DurationMs { get; set; } = 0;
}
