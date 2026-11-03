#nullable enable

using System;

namespace Umami.Models;

public partial class SessionLink
{
    public Guid WebsiteId { get; set; }

    public Guid SessionId { get; set; }

    public string DistinctId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
