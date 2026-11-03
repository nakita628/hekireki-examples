#nullable enable

namespace Calcom.Models;

public partial class Tracking
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public string? UtmSource { get; set; }

    public string? UtmMedium { get; set; }

    public string? UtmCampaign { get; set; }

    public string? UtmTerm { get; set; }

    public string? UtmContent { get; set; }
}
