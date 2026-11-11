#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMShadowEvalJob
{
    public string Id { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public string TargetType { get; set; } = "key";

    public string TargetId { get; set; } = null!;

    public string RouterName { get; set; } = null!;

    public List<string>? RouterNames { get; set; } = new List<string>();

    public List<string>? Models { get; set; } = new List<string>();

    public string Direction { get; set; } = "forward";

    public string? BaselineModel { get; set; }

    public string JudgeModel { get; set; } = null!;

    public double ShadowPercentage { get; set; }

    public int MaxTurns { get; set; }

    public double? MaxBudget { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime EndsAt { get; set; }

    public DateTime? StoppedAt { get; set; }

    public string? StoppedBy { get; set; }
}
