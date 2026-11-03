#nullable enable

using System;

namespace Calcom.Models;

public partial class WatchlistAudit
{
    public Guid Id { get; set; }

    public WatchlistType Type { get; set; }

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public WatchlistAction Action { get; set; } = WatchlistAction.Report;

    public DateTime ChangedAt { get; set; }

    public int? ChangedByUserId { get; set; }

    public Guid? WatchlistId { get; set; }

    public virtual Watchlist? Watchlist { get; set; }
}
