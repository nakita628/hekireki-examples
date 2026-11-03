#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum WatchlistType
{
    [PgName("EMAIL")]
    Email,

    [PgName("DOMAIN")]
    Domain,

    [PgName("USERNAME")]
    Username,
}
