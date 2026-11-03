#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class OrganizationMembership
{
    public string Id { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public Role Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ProjectMembership> ProjectMemberships { get; set; } = new List<ProjectMembership>();
}
