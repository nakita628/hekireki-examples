#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum PricingModel
{
    [PgName("TIERED")]
    Tiered,

    [PgName("SEAT_EVENT")]
    SeatEvent,
}
