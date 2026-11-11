#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMOrganizationMembership
{
    public string UserId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string? UserRole { get; set; }

    public double? Spend { get; set; } = 0.0;

    public string? BudgetId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual LiteLLMUserTable User { get; set; } = null!;

    public virtual LiteLLMOrganizationTable Organization { get; set; } = null!;

    public virtual LiteLLMBudgetTable? LitellmBudgetTable { get; set; }
}
