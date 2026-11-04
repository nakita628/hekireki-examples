#nullable enable

using System;
using System.Collections.Generic;

namespace AnythingLLM.Models;

public partial class ScheduledJobs
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Prompt { get; set; } = null!;

    public string? Tools { get; set; }

    public string Schedule { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public DateTime? LastRunAt { get; set; }

    public DateTime? NextRunAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ScheduledJobRuns> Runs { get; set; } = new List<ScheduledJobRuns>();
}
