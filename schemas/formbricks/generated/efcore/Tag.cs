#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Tag
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TagsOnResponses> Responses { get; set; } = new List<TagsOnResponses>();

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;
}
