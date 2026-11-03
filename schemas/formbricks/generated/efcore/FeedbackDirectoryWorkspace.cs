#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class FeedbackDirectoryWorkspace
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string FeedbackDirectoryId { get; set; } = null!;

    public virtual FeedbackDirectory FeedbackDirectory { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public virtual ICollection<FeedbackSource> FeedbackSources { get; set; } = new List<FeedbackSource>();
}
