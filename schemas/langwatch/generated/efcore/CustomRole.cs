#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class CustomRole
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Permissions { get; set; } = null!;

    public string Kind { get; set; } = "custom";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<TeamUser> AssignedUsers { get; set; } = new List<TeamUser>();

    public virtual ICollection<RoleBinding> RoleBindings { get; set; } = new List<RoleBinding>();
}
