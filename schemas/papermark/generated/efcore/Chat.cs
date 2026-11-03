#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Chat
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string? DocumentId { get; set; }

    public virtual Document? Document { get; set; }

    public string? DataroomId { get; set; }

    public virtual Dataroom? Dataroom { get; set; }

    public string? LinkId { get; set; }

    public virtual Link? Link { get; set; }

    public string? ViewId { get; set; }

    public virtual View? View { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? VectorStoreId { get; set; }

    public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastMessageAt { get; set; }
}
