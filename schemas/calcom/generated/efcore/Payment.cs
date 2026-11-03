#nullable enable

namespace Calcom.Models;

public partial class Payment
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public virtual App? App { get; set; }

    public string? AppId { get; set; }

    public int BookingId { get; set; }

    public virtual Booking? Booking { get; set; }

    public int Amount { get; set; }

    public int Fee { get; set; }

    public string Currency { get; set; } = null!;

    public bool Success { get; set; }

    public bool Refunded { get; set; }

    public string Data { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public PaymentOption? PaymentOption { get; set; } = global::Calcom.Models.PaymentOption.OnBooking;
}
