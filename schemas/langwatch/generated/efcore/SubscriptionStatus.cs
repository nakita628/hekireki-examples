#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum SubscriptionStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("FAILED")]
    Failed,

    [PgName("ACTIVE")]
    Active,

    [PgName("CANCELLED")]
    Cancelled,
}
