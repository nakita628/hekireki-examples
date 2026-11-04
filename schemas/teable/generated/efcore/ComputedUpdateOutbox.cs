#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class ComputedUpdateOutbox
{
    public string Id { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string WorkerLane { get; set; } = "production";

    public string SeedTableId { get; set; } = null!;

    public string? SeedRecordIds { get; set; }

    public string ChangeType { get; set; } = null!;

    public string? Steps { get; set; }

    public string? Edges { get; set; }

    public string Status { get; set; } = null!;

    public int Attempts { get; set; } = 0;

    public int MaxAttempts { get; set; } = 8;

    public DateTime NextRunAt { get; set; }

    public DateTime? LockedAt { get; set; }

    public string? LockedBy { get; set; }

    public string? LastError { get; set; }

    public int EstimatedComplexity { get; set; } = 0;

    public string PlanHash { get; set; } = null!;

    public string? DirtyStats { get; set; }

    public string RunId { get; set; } = null!;

    public List<string>? OriginRunIds { get; set; } = new List<string>();

    public int RunTotalSteps { get; set; } = 0;

    public int RunCompletedStepsBefore { get; set; } = 0;

    public List<string>? AffectedTableIds { get; set; } = new List<string>();

    public List<string>? AffectedFieldIds { get; set; } = new List<string>();

    public int? SyncMaxLevel { get; set; }

    public DateTime? SourceChangedAt { get; set; }

    public int StageDepth { get; set; } = 0;

    public string? PredecessorTaskId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ComputedUpdateOutboxSeed> Seeds { get; set; } = new List<ComputedUpdateOutboxSeed>();
}
