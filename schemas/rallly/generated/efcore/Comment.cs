#nullable enable

using System;

namespace Rallly.Models;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string PollId { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Poll Poll { get; set; } = null!;

    public virtual User? User { get; set; }
}
