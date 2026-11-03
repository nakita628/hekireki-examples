#nullable enable

using System;

namespace Calcom.Models;

public partial class InstantMeetingToken
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public int? BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
