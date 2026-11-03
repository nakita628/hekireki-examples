#nullable enable

using System;

namespace Teable.Models;

public partial class Invitation
{
    public string Id { get; set; } = null!;

    public string? BaseId { get; set; }

    public string? SpaceId { get; set; }

    public string Type { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string InvitationCode { get; set; } = null!;

    public DateTime? ExpiredTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? DeletedTime { get; set; }
}
