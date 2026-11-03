#nullable enable

using System;

namespace Rallly.Models;

public partial class SpaceMemberInvite
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public SpaceMemberRole Role { get; set; } = SpaceMemberRole.Member;

    public string InviterId { get; set; } = null!;

    public virtual User InvitedBy { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;
}
