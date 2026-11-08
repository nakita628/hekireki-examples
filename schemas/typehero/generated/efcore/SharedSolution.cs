#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class SharedSolution
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsPinned { get; set; } = false;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Vote> Vote { get; set; } = new List<Vote>();

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public virtual Challenge? Challenge { get; set; }

    public int? ChallengeId { get; set; }

    public virtual ICollection<Comment> SolutionComment { get; set; } = new List<Comment>();

    public virtual ICollection<Report> Report { get; set; } = new List<Report>();

    public virtual ICollection<Notification> Notification { get; set; } = new List<Notification>();
}
