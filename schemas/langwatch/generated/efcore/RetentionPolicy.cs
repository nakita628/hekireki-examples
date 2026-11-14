#nullable enable

using System;

namespace LangWatch.Models;

public partial class RetentionPolicy
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public RetentionPolicyScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int RetentionDays { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
