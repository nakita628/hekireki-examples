#nullable enable

using System;

namespace Calcom.Models;

public partial class BookingReport
{
    public Guid Id { get; set; }

    public string BookingUid { get; set; } = null!;

    public virtual Booking Booking { get; set; } = null!;

    public string BookerEmail { get; set; } = null!;

    public int? ReportedById { get; set; }

    public int? OrganizationId { get; set; }

    public BookingReportReason Reason { get; set; }

    public string? Description { get; set; }

    public bool Cancelled { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public BookingReportStatus Status { get; set; } = BookingReportStatus.Pending;

    public SystemReportStatus SystemStatus { get; set; } = SystemReportStatus.Pending;

    public Guid? WatchlistId { get; set; }

    public Guid? GlobalWatchlistId { get; set; }

    public virtual User? ReportedBy { get; set; }

    public virtual Team? Organization { get; set; }

    public virtual Watchlist? Watchlist { get; set; }

    public virtual Watchlist? GlobalWatchlist { get; set; }
}
