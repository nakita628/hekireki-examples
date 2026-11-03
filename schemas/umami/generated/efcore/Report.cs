#nullable enable

using System;

namespace Umami.Models;

public partial class Report
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid WebsiteId { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Parameters { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Website Website { get; set; } = null!;
}
