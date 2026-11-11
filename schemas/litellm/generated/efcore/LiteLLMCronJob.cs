#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMCronJob
{
    public string CronjobId { get; set; } = null!;

    public string PodId { get; set; } = null!;

    public JobStatus Status { get; set; } = JobStatus.Inactive;

    public DateTime LastUpdated { get; set; }

    public DateTime Ttl { get; set; }
}
