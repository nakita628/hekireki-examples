#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Segment
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsPrivate { get; set; } = true;

    public string Filters { get; set; } = "[]";

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
