#nullable enable

using System;

namespace LangWatch.Models;

public partial class RoleBinding
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public string? ApiKeyId { get; set; }

    public virtual ApiKey? ApiKey { get; set; }

    public TeamUserRole Role { get; set; }

    public string? CustomRoleId { get; set; }

    public virtual CustomRole? CustomRole { get; set; }

    public RoleBindingScopeType ScopeType { get; set; }

    public string ScopeId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
