#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public bool Visible { get; set; } = true;

    public int? ParentId { get; set; }

    public virtual Comment? ParentComment { get; set; }

    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public CommentRoot RootType { get; set; } = CommentRoot.Challenge;

    public int? RootChallengeId { get; set; }

    public virtual Challenge? RootChallenge { get; set; }

    public int? RootSolutionId { get; set; }

    public virtual SharedSolution? RootSolution { get; set; }

    public virtual ICollection<Report> Report { get; set; } = new List<Report>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Vote> Vote { get; set; } = new List<Vote>();

    public virtual ICollection<Notification> Notification { get; set; } = new List<Notification>();
}
