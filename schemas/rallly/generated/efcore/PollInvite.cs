#nullable enable

using System;

namespace Rallly.Models;

public partial class PollInvite
{
    public string Id { get; set; } = null!;

    public string PollId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime? OpenedAt { get; set; }

    public DateTime? RemindedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? ParticipantId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Poll Poll { get; set; } = null!;

    public virtual Participant? Participant { get; set; }
}
