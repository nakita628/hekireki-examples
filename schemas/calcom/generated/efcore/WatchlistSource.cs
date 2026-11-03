#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum WatchlistSource
{
    [PgName("MANUAL")]
    Manual,

    [PgName("FREE_DOMAIN_POLICY")]
    FreeDomainPolicy,

    [PgName("SIGNUP")]
    Signup,
}
