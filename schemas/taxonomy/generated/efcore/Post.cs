#nullable enable

using System;

namespace Taxonomy.Models;

public partial class Post
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public bool Published { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string AuthorId { get; set; } = null!;

    public virtual User Author { get; set; } = null!;
}
