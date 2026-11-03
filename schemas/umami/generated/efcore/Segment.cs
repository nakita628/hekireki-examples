#nullable enable

using System;

namespace Umami.Models;

public partial class Segment
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Parameters { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual Website Website { get; set; } = null!;
}
