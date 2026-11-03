#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class VisitorGroup
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public List<string>? Emails { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<LinkVisitorGroup> Links { get; set; } = new List<LinkVisitorGroup>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
