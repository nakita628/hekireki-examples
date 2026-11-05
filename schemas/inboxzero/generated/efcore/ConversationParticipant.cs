#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ConversationParticipant
{
    public string Id { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public string MemberId { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public int Generation { get; set; }

    public bool Active { get; set; } = true;

    public int ReadRevision { get; set; } = 0;

    public bool Muted { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ConversationActivity> Activity { get; set; } = new List<ConversationActivity>();
}
