#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMLensTraceSignal
{
    public string TraceId { get; set; } = null!;

    public string TraceRef { get; set; } = "";

    public string ConfigKey { get; set; } = null!;

    public int SpanCount { get; set; }

    public DateTime? ClaimedUntil { get; set; }

    public DateTime? ClassifiedAt { get; set; }

    public string Data { get; set; } = null!;
}
