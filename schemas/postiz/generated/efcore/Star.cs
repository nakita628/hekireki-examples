#nullable enable

using System;

namespace Postiz.Models;

public partial class Star
{
    public string Id { get; set; } = null!;

    public int Stars { get; set; }

    public int TotalStars { get; set; }

    public int Forks { get; set; }

    public int TotalForks { get; set; }

    public string Login { get; set; } = null!;

    public DateOnly Date { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
