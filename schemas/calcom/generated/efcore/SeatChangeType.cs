#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum SeatChangeType
{
    [PgName("ADDITION")]
    Addition,

    [PgName("REMOVAL")]
    Removal,
}
