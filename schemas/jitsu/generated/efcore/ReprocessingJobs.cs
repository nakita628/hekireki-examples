#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class ReprocessingJobs
{
    public string Id { get; set; } = null!;

    public string Config { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? K8sJobName { get; set; }

    public int? TotalFiles { get; set; } = 0;

    public int? TotalWorkers { get; set; } = 0;

    public string? Error { get; set; }

    public virtual ICollection<ReprocessingWorkers> ReprocessingWorkers { get; set; } = new List<ReprocessingWorkers>();
}
