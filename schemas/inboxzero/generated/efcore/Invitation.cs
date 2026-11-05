#nullable enable

using System;

namespace InboxZero.Models;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Role { get; set; }

    public string Status { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public string InviterId { get; set; } = null!;

    public virtual EmailAccount Inviter { get; set; } = null!;
}
