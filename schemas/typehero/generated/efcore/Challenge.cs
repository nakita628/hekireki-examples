#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Challenge
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Difficulty Difficulty { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Tests { get; set; } = null!;

    public ChallengeStatus Status { get; set; } = ChallengeStatus.Pending;

    public string? Tsconfig { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Vote> Vote { get; set; } = new List<Vote>();

    public virtual ICollection<Submission> Submission { get; set; } = new List<Submission>();

    public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();

    public virtual ICollection<Bookmark> Bookmark { get; set; } = new List<Bookmark>();

    public virtual ICollection<SharedSolution> SharedSolution { get; set; } = new List<SharedSolution>();

    public virtual ICollection<Report> Report { get; set; } = new List<Report>();

    public virtual ICollection<TrackChallenge> TrackChallenge { get; set; } = new List<TrackChallenge>();

    public virtual ICollection<Notification> Notification { get; set; } = new List<Notification>();
}
