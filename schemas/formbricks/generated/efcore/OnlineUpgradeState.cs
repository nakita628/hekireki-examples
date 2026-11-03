#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class OnlineUpgradeState
{
    public string Release { get; set; } = null!;

    public string TargetImage { get; set; } = null!;

    public string Phase { get; set; } = null!;

    public List<string>? CompletedPhases { get; set; } = new List<string>();

    public string? SurveyCursor { get; set; }

    public long SurveyCount { get; set; } = 0L;

    public DateTime? BaselineCompletedAt { get; set; }

    public DateTime? ForwardOnlyAt { get; set; }

    public DateTime? QueuePausedAt { get; set; }

    public DateTime? QueuePauseDeadlineAt { get; set; }

    public DateTime? QueueResumedAt { get; set; }

    public DateTime? ActivatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
