#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class FeedbackDirectory
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public bool IsArchived { get; set; } = false;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<FeedbackDirectoryWorkspace> Workspaces { get; set; } = new List<FeedbackDirectoryWorkspace>();

    public virtual ICollection<FeedbackSource> FeedbackSources { get; set; } = new List<FeedbackSource>();

    public virtual ICollection<Chart> Charts { get; set; } = new List<Chart>();
}
