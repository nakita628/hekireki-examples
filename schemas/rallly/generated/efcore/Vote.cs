#nullable enable

using System;

namespace Rallly.Models;

public partial class Vote
{
    public string Id { get; set; } = null!;

    public string ParticipantId { get; set; } = null!;

    public string OptionId { get; set; } = null!;

    public string PollId { get; set; } = null!;

    public VoteType Type { get; set; } = VoteType.Yes;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Participant Participant { get; set; } = null!;

    public virtual Option Option { get; set; } = null!;

    public virtual Poll Poll { get; set; } = null!;
}
