#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMTelemetryReport
{
    public string Id { get; set; } = null!;

    public double WindowStart { get; set; }

    public double WindowEnd { get; set; }

    public string Report { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
