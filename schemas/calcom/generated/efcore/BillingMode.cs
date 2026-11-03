#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BillingMode
{
    [PgName("SEATS")]
    Seats,

    [PgName("ACTIVE_USERS")]
    ActiveUsers,
}
