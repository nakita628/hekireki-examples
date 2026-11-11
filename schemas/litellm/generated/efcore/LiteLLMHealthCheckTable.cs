#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMHealthCheckTable
{
    public string HealthCheckId { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public string? ModelId { get; set; }

    public string Status { get; set; } = null!;

    public int HealthyCount { get; set; } = 0;

    public int UnhealthyCount { get; set; } = 0;

    public string? ErrorMessage { get; set; }

    public double? ResponseTimeMs { get; set; }

    public string? Details { get; set; }

    public string? CheckedBy { get; set; }

    public DateTime CheckedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
