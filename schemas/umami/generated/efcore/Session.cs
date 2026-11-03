#nullable enable

using System;
using System.Collections.Generic;

namespace Umami.Models;

public partial class Session
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public string? Browser { get; set; }

    public string? Os { get; set; }

    public string? Device { get; set; }

    public string? Screen { get; set; }

    public string? Language { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? City { get; set; }

    public string? DistinctId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<WebsiteEvent> WebsiteEvents { get; set; } = new List<WebsiteEvent>();

    public virtual ICollection<SessionData> SessionData { get; set; } = new List<SessionData>();

    public virtual ICollection<Revenue> Revenue { get; set; } = new List<Revenue>();
}
