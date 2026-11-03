#nullable enable

using System;

namespace Umami.Models;

public partial class TeamUser
{
    public Guid Id { get; set; }

    public Guid TeamId { get; set; }

    public Guid UserId { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
