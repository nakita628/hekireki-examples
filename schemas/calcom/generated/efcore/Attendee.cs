#nullable enable

namespace Calcom.Models;

public partial class Attendee
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TimeZone { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Locale { get; set; } = "en";

    public virtual Booking? Booking { get; set; }

    public int? BookingId { get; set; }

    public virtual BookingSeat? BookingSeat { get; set; }

    public bool? NoShow { get; set; } = false;
}
