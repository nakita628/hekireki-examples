#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Creative
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public CreativeType Type { get; set; }

    public CreativeStatus Status { get; set; } = CreativeStatus.Draft;

    public DateTime? PublishedAt { get; set; }

    public DateTime? ArchivedAt { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual User? Creator { get; set; }

    public string? CreatedBy { get; set; }

    public string Content { get; set; } = null!;

    public string Styling { get; set; } = null!;

    public string? SurveyId { get; set; }

    public virtual Survey? Survey { get; set; }

    public string? LinkUrl { get; set; }

    public CreativeShowOn ShowOn { get; set; } = CreativeShowOn.PageView;

    public string? ShowConditions { get; set; }

    public double? DisplayPercentage { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public bool? AutoSelectLanguage { get; set; }

    public virtual ICollection<CreativeTrigger> Triggers { get; set; } = new List<CreativeTrigger>();

    public virtual ICollection<CreativeLanguage> Languages { get; set; } = new List<CreativeLanguage>();

    public virtual ICollection<CreativeDailyStat> DailyStats { get; set; } = new List<CreativeDailyStat>();

    public virtual ICollection<CreativeDailyPageStat> DailyPageStats { get; set; } = new List<CreativeDailyPageStat>();
}
