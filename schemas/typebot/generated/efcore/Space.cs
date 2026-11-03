#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class Space
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Icon { get; set; }

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public virtual ICollection<Typebot> Typebots { get; set; } = new List<Typebot>();
}
