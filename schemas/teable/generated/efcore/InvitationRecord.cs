#nullable enable

using System;

namespace Teable.Models;

public partial class InvitationRecord
{
    public string Id { get; set; } = null!;

    public string InvitationId { get; set; } = null!;

    public string? BaseId { get; set; }

    public string? SpaceId { get; set; }

    public string Type { get; set; } = null!;

    public string Inviter { get; set; } = null!;

    public string Accepter { get; set; } = null!;

    public DateTime CreatedTime { get; set; }
}
