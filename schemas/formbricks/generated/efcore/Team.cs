#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Team
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();

    public virtual ICollection<WorkspaceTeam> WorkspaceTeams { get; set; } = new List<WorkspaceTeam>();
}
