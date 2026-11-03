#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class Poll
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? Deadline { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? Conferencing { get; set; }

    public string? UserId { get; set; }

    public string? TimeZone { get; set; }

    public PollStatus Status { get; set; } = PollStatus.Open;

    public PollClosedReason? ClosedReason { get; set; }

    public PollKind Kind { get; set; } = PollKind.Date;

    public bool Deleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public string? EventId { get; set; }

    public string? ScheduledEventId { get; set; }

    public bool HideParticipants { get; set; } = false;

    public bool HideScores { get; set; } = false;

    public bool DisableComments { get; set; } = true;

    public bool RequireParticipantEmail { get; set; } = false;

    public bool AllowTentativeVotes { get; set; } = true;

    public bool Muted { get; set; } = false;

    public virtual User? User { get; set; }

    public virtual ScheduledEvent? ScheduledEvent { get; set; }

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public virtual ICollection<PollInvite> Invites { get; set; } = new List<PollInvite>();

    public virtual ICollection<PollActivity> Activities { get; set; } = new List<PollActivity>();

    public virtual Space? Space { get; set; }

    public string? SpaceId { get; set; }
}
