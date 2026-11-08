#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum CardVerificationStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("VERIFIED")]
    Verified,

    [PgName("REQUIRES_ACTION")]
    RequiresAction,

    [PgName("FAILED")]
    Failed,
}
