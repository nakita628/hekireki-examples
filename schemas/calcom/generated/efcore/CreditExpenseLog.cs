#nullable enable

using System;

namespace Calcom.Models;

public partial class CreditExpenseLog
{
    public string Id { get; set; } = null!;

    public string CreditBalanceId { get; set; } = null!;

    public virtual CreditBalance CreditBalance { get; set; } = null!;

    public string? BookingUid { get; set; }

    public virtual Booking? Booking { get; set; }

    public int? Credits { get; set; }

    public CreditType CreditType { get; set; }

    public DateTime Date { get; set; }

    public string? SmsSid { get; set; }

    public int? SmsSegments { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int? CallDuration { get; set; }

    public CreditUsageType? CreditFor { get; set; }

    public string? ExternalRef { get; set; }
}
