#nullable enable

using System;

namespace Calcom.Models;

public partial class AssignmentReason
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int BookingId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public AssignmentReasonEnum ReasonEnum { get; set; }

    public string ReasonString { get; set; } = null!;
}
