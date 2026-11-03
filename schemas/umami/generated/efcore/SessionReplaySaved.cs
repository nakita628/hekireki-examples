#nullable enable

using System;

namespace Umami.Models;

public partial class SessionReplaySaved
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid WebsiteId { get; set; }

    public Guid VisitId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual Website Website { get; set; } = null!;
}
