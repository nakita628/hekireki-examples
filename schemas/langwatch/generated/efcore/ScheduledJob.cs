#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScheduledJob
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string TargetType { get; set; } = null!;

    public string TargetId { get; set; } = null!;

    public string Cron { get; set; } = null!;

    public string Timezone { get; set; } = null!;

    public DateTime NextRunAt { get; set; }

    public DateTime? LastSlot { get; set; }

    public DateTime? CurrentSlot { get; set; }

    public int Attempts { get; set; } = 0;

    public string? LastError { get; set; }

    public bool Active { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
