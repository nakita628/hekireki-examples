#nullable enable

using System;

namespace LangWatch.Models;

public partial class Grant
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public GrantPrincipalType PrincipalType { get; set; }

    public string? PrincipalId { get; set; }

    public string? RoleKey { get; set; }

    public string? LegacyRole { get; set; }

    public string Source { get; set; } = null!;

    public GrantScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public string? Token { get; set; }

    public string? Permission { get; set; }

    public string? ResourceKind { get; set; }

    public string? ProjectId { get; set; }

    public string? CreatedByUserId { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int? MaxViews { get; set; }

    public string? Condition { get; set; }

    public string? ReplacesGrantId { get; set; }

    public DateTime OccurredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? RevokedReason { get; set; }
}
