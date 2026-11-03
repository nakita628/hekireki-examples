#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public CommentObjectType ObjectType { get; set; }

    public string ObjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Content { get; set; } = null!;

    public string? AuthorUserId { get; set; }

    public virtual ICollection<CommentReaction> Reactions { get; set; } = new List<CommentReaction>();

    public string? DataField { get; set; }

    public List<string>? Path { get; set; } = new List<string>();

    public List<int>? RangeStart { get; set; } = new List<int>();

    public List<int>? RangeEnd { get; set; } = new List<int>();
}
