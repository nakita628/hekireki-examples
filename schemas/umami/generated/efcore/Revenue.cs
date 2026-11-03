#nullable enable

using System;

namespace Umami.Models;

public partial class Revenue
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid SessionId { get; set; }

    public Guid EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public decimal? Revenue1 { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Website Website { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
