#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class DashboardFolder
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? ParentFolderId { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual DashboardFolder? ParentFolder { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public virtual ICollection<DashboardFolder> ChildrenFolder { get; set; } = new List<DashboardFolder>();

    public virtual ICollection<Typebot> Typebots { get; set; } = new List<Typebot>();
}
