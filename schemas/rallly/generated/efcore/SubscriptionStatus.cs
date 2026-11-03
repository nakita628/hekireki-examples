#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum SubscriptionStatus
{
    [PgName("incomplete")]
    Incomplete,

    [PgName("incomplete_expired")]
    IncompleteExpired,

    [PgName("active")]
    Active,

    [PgName("paused")]
    Paused,

    [PgName("trialing")]
    Trialing,

    [PgName("past_due")]
    PastDue,

    [PgName("canceled")]
    Canceled,

    [PgName("unpaid")]
    Unpaid,
}
