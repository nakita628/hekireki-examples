#nullable enable

using System;

namespace Postiz.Models;

public partial class Mentions
{
    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Image { get; set; } = null!;
}
