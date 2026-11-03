#nullable enable

using System;

namespace Rallly.Models;

public partial class SpaceMember
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public SpaceMemberRole Role { get; set; } = SpaceMemberRole.Member;

    public DateTime LastSelectedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
