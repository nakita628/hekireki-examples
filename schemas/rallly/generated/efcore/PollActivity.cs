#nullable enable

using System;

namespace Rallly.Models;

public partial class PollActivity
{
    public string Id { get; set; } = null!;

    public string PollId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? UserId { get; set; }

    public string? ParticipantId { get; set; }

    public string? InviteId { get; set; }

    public string? OptionId { get; set; }

    public string Payload { get; set; } = "{}";

    public DateTime CreatedAt { get; set; }

    public virtual Poll Poll { get; set; } = null!;
}
