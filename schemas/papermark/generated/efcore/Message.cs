#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Message
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? ViewId { get; set; }

    public virtual View? View { get; set; }

    public bool IsRead { get; set; } = false;

    public bool IsInternalNote { get; set; } = false;

    public virtual ICollection<DataroomFaqItem> FaqAsQuestion { get; set; } = new List<DataroomFaqItem>();

    public virtual ICollection<DataroomFaqItem> FaqAsAnswer { get; set; } = new List<DataroomFaqItem>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
