#nullable enable

using System;
using System.Collections.Generic;

namespace TypeHero.Models;

public partial class Report
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public ReportType Type { get; set; }

    public ReportStatus Status { get; set; }

    public virtual ICollection<ReportIssue> Issues { get; set; } = new List<ReportIssue>();

    public string ReporterId { get; set; } = null!;

    public virtual User Reporter { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public int? ChallengeId { get; set; }

    public virtual Challenge? Challenge { get; set; }

    public int? CommentId { get; set; }

    public virtual Comment? Comment { get; set; }

    public int? SolutionId { get; set; }

    public virtual SharedSolution? Solution { get; set; }

    public string? ModeratorId { get; set; }

    public virtual User? Moderator { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
