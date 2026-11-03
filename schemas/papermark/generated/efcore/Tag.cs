#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Tag
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string? Description { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<TagItem> Items { get; set; } = new List<TagItem>();

    public string? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
