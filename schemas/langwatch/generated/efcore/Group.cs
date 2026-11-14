#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Group
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? ExternalId { get; set; }

    public string? ScimSource { get; set; }

    public string? ScimConnectionId { get; set; }

    public virtual ICollection<GroupMembership> Members { get; set; } = new List<GroupMembership>();

    public virtual ICollection<RoleBinding> RoleBindings { get; set; } = new List<RoleBinding>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
