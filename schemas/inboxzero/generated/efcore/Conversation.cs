#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Conversation
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string PublisherId { get; set; } = null!;

    public virtual Member Publisher { get; set; } = null!;

    public string ThreadId { get; set; } = null!;

    public ConversationStatus Status { get; set; } = ConversationStatus.Active;

    public int Revision { get; set; } = 0;

    public int Generation { get; set; } = 1;

    public virtual ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<ConversationComment> Comments { get; set; } = new List<ConversationComment>();
}
