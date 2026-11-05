#nullable enable

using System;

namespace InboxZero.Models;

public partial class ConversationActivity
{
    public string Id { get; set; } = null!;

    public string ParticipantId { get; set; } = null!;

    public virtual ConversationParticipant Participant { get; set; } = null!;

    public string? CommentId { get; set; }

    public virtual ConversationComment? Comment { get; set; }

    public ConversationActivityKind Kind { get; set; }

    public int Revision { get; set; }

    public DateTime CreatedAt { get; set; }
}
