#nullable enable

using System;

namespace LangWatch.Models;

public partial class NurturingProjectTraceDay
{
    public string ProjectId { get; set; } = null!;

    public string Day { get; set; } = null!;

    public int TraceCount { get; set; } = 0;

    public int ErrorCount { get; set; } = 0;

    public DateTime? LastTraceAt { get; set; }

    public DateTime? LastFailedTraceAt { get; set; }
}
