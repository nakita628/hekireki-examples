#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public string? Description { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public virtual ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public RoleType Type { get; set; } = RoleType.Custom;
}
