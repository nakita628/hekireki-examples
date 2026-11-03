#nullable enable

using System;

namespace Langfuse.Models;

public partial class MembershipInvitation
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public Role OrgRole { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public Role? ProjectRole { get; set; }

    public string? InvitedByUserId { get; set; }

    public virtual User? InvitedByUser { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
