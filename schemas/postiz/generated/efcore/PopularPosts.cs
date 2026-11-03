#nullable enable

using System;

namespace Postiz.Models;

public partial class PopularPosts
{
    public string Id { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Hook { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
