#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum SubscriptionStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("PAST_DUE")]
    PastDue,

    [PgName("INACTIVE")]
    Inactive,
}
