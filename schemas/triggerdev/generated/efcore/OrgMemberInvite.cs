#nullable enable

using System;

namespace Trigger.Models;

public partial class OrgMemberInvite
{
    public string Id { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string Email { get; set; } = null!;

    public OrgMemberRole Role { get; set; } = OrgMemberRole.Member;

    public string? RbacRoleId { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual User Inviter { get; set; } = null!;

    public string InviterId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
