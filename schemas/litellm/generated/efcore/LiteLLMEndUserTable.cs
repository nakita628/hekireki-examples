#nullable enable

using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMEndUserTable
{
    public string UserId { get; set; } = null!;

    public string? Alias { get; set; }

    public double Spend { get; set; } = 0.0;

    public string? AllowedModelRegion { get; set; }

    public string? DefaultModel { get; set; }

    public List<string>? Models { get; set; } = new List<string>();

    public string? BudgetId { get; set; }

    public string? ObjectPermissionId { get; set; }

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }

    public bool Blocked { get; set; } = false;
}
