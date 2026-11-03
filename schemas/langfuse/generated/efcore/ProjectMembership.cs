#nullable enable

using System;

namespace Langfuse.Models;

public partial class ProjectMembership
{
    public string OrgMembershipId { get; set; } = null!;

    public virtual OrganizationMembership OrganizationMembership { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public Role Role { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
