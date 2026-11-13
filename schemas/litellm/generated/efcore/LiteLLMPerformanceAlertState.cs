#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMPerformanceAlertState
{
    public bool Enabled { get; set; } = true;

    public int? AlertsUsed { get; set; }

    public int Id { get; set; }

    public string Owner { get; set; } = null!;

    public DateTime NextRunAt { get; set; }

    public string? Day { get; set; }

    public string? Scope { get; set; }

    public string ResumeAfter { get; set; } = "";

    public bool Wrapped { get; set; } = false;

    public int Checked { get; set; } = 0;

    public int Skipped { get; set; } = 0;

    public string? StartScope { get; set; }

    public string StartAfter { get; set; } = "";

    public DateTime? CompletedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
