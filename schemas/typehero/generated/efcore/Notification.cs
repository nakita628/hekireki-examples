#nullable enable

using System;

namespace TypeHero.Models;

public partial class Notification
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsRead { get; set; } = false;

    public NotificationType Type { get; set; }

    public virtual User ToUser { get; set; } = null!;

    public string ToUserId { get; set; } = null!;

    public virtual User FromUser { get; set; } = null!;

    public string FromUserId { get; set; } = null!;

    public int? CommentId { get; set; }

    public virtual Comment? Comment { get; set; }

    public string? Content { get; set; }

    public string? Blurb { get; set; }

    public string Url { get; set; } = null!;

    public virtual Challenge? Challenge { get; set; }

    public int? ChallengeId { get; set; }

    public virtual SharedSolution? SharedSolution { get; set; }

    public int? SharedSolutionId { get; set; }
}
