#nullable enable

using System;

namespace TypeHero.Models;

public partial class ShortURL
{
    public int Id { get; set; }

    public string OriginalUrl { get; set; } = null!;

    public string ShortUrlSlug { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int Clicks { get; set; } = 0;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }
}
