#nullable enable

using System;

namespace Langfuse.Models;

public partial class CronJobs
{
    public string Name { get; set; } = null!;

    public DateTime? LastRun { get; set; }

    public DateTime? JobStartedAt { get; set; }

    public string? State { get; set; }
}
