#nullable enable

using System;

namespace Calcom.Models;

public partial class WrongAssignmentReport
{
    public Guid Id { get; set; }

    public string BookingUid { get; set; } = null!;

    public virtual Booking Booking { get; set; } = null!;

    public int? ReportedById { get; set; }

    public virtual User? ReportedBy { get; set; }

    public string? CorrectAssignee { get; set; }

    public string AdditionalNotes { get; set; } = null!;

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public WrongAssignmentReportStatus Status { get; set; } = WrongAssignmentReportStatus.Pending;

    public int? ReviewedById { get; set; }

    public virtual User? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
