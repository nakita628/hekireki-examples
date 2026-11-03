#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class Tag
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Color { get; set; } = "blue";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public virtual ICollection<LinkTag> Links { get; set; } = new List<LinkTag>();
}
