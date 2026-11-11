#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMProjectTable
{
    public string ProjectId { get; set; } = null!;

    public string? ProjectAlias { get; set; }

    public string? Description { get; set; }

    public string? TeamId { get; set; }

    public string? BudgetId { get; set; }

    public string Metadata { get; set; } = "{}";

    public List<string>? Models { get; set; }

    public double Spend { get; set; } = 0.0;

    public string ModelSpend { get; set; } = "{}";

    public string ModelRpmLimit { get; set; } = "{}";

    public string ModelTpmLimit { get; set; } = "{}";

    public bool Blocked { get; set; } = false;

    public string? ObjectPermissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual LiteLLMTeamTable? LitellmTeamTable { get; set; }

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }

    public virtual ICollection<LiteLLMVerificationToken> Keys { get; set; } = new List<LiteLLMVerificationToken>();

    public virtual LiteLLMObjectPermissionTable? ObjectPermission { get; set; }
}
