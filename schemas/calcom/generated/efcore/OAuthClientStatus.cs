#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum OAuthClientStatus
{
    [PgName("pending")]
    Pending,

    [PgName("approved")]
    Approved,

    [PgName("rejected")]
    Rejected,
}
