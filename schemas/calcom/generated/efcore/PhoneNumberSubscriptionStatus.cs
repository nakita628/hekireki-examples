#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum PhoneNumberSubscriptionStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("PAST_DUE")]
    PastDue,

    [PgName("CANCELLED")]
    Cancelled,

    [PgName("INCOMPLETE")]
    Incomplete,

    [PgName("INCOMPLETE_EXPIRED")]
    IncompleteExpired,

    [PgName("TRIALING")]
    Trialing,

    [PgName("UNPAID")]
    Unpaid,
}
