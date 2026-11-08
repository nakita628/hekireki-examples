#nullable enable

using System;

namespace TypeHero.Models;

public partial class Bookmark
{
    public int Id { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual Challenge? Challenge { get; set; }

    public int? ChallengeId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
