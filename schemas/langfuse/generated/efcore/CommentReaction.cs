#nullable enable

using System;

namespace Langfuse.Models;

public partial class CommentReaction
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string CommentId { get; set; } = null!;

    public virtual Comment Comment { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string Emoji { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
