#nullable enable

using System;

namespace Jitsu.Models;

public partial class InvitationToken
{
    public string Id { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Token { get; set; } = null!;

    public string? UsedBy { get; set; }

    public WorkspaceRole Role { get; set; } = WorkspaceRole.Owner;
}
