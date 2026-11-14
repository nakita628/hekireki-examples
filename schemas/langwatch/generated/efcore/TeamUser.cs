#nullable enable

using System;

namespace LangWatch.Models;

public partial class TeamUser
{
    public string UserId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public TeamUserRole Role { get; set; }

    public string? AssignedRoleId { get; set; }

    public virtual CustomRole? AssignedRole { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
