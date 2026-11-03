#nullable enable

namespace Calcom.Models;

public partial class BookingSeat
{
    public int Id { get; set; }

    public string ReferenceUid { get; set; } = null!;

    public int BookingId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public int AttendeeId { get; set; }

    public virtual Attendee Attendee { get; set; } = null!;

    public string? Data { get; set; }

    public string? Metadata { get; set; }
}
