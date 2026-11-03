#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum AccessScope
{
    [PgName("READ_BOOKING")]
    ReadBooking,

    [PgName("READ_PROFILE")]
    ReadProfile,
}
