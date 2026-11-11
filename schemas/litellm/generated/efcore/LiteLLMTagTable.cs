#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMTagTable
{
    public string TagName { get; set; } = null!;

    public string? Description { get; set; }

    public List<string>? Models { get; set; }

    public string? ModelInfo { get; set; }

    public double Spend { get; set; } = 0.0;

    public string? BudgetId { get; set; }

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }
}
