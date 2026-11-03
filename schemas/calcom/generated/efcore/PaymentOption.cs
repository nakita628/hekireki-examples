#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum PaymentOption
{
    [PgName("ON_BOOKING")]
    OnBooking,

    [PgName("HOLD")]
    Hold,
}
