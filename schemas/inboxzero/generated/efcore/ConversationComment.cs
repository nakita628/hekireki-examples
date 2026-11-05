#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ConversationComment
{
    public string Id { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public string? AuthorMemberId { get; set; }

    public virtual Member? Author { get; set; }

    public string ClientMutationId { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int Revision { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ConversationActivity> Activity { get; set; } = new List<ConversationActivity>();
}
