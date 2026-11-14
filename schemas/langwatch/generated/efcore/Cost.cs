#nullable enable

using System;

namespace LangWatch.Models;

public partial class Cost
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public CostType CostType { get; set; }

    public string? CostName { get; set; }

    public CostReferenceType ReferenceType { get; set; }

    public string ReferenceId { get; set; } = null!;

    public double Amount { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? ExtraInfo { get; set; }
}
