#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMGuardrailsTable
{
    public string GuardrailId { get; set; } = null!;

    public string GuardrailName { get; set; } = null!;

    public string LitellmParams { get; set; } = null!;

    public string? GuardrailInfo { get; set; }

    public string? TeamId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Status { get; set; } = "active";

    public DateTime? SubmittedAt { get; set; }

    public DateTime? ReviewedAt { get; set; }
}
