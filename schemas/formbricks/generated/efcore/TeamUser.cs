#nullable enable

using System;

namespace Formbricks.Models;

public partial class TeamUser
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public TeamUserRole Role { get; set; }
}
