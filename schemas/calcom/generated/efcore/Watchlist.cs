#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Watchlist
{
    public Guid Id { get; set; }

    public WatchlistType Type { get; set; }

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsGlobal { get; set; } = false;

    public int? OrganizationId { get; set; }

    public WatchlistAction Action { get; set; } = WatchlistAction.Report;

    public WatchlistSource Source { get; set; } = WatchlistSource.Manual;

    public DateTime LastUpdatedAt { get; set; }

    public virtual ICollection<BookingReport> BookingReports { get; set; } = new List<BookingReport>();

    public virtual ICollection<BookingReport> GlobalBookingReports { get; set; } = new List<BookingReport>();

    public virtual ICollection<WatchlistAudit> Audits { get; set; } = new List<WatchlistAudit>();
}
