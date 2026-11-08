#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum SubscriptionStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("TRIAL")]
    Trial,

    [PgName("CANCELLED")]
    Cancelled,

    [PgName("EXPIRED")]
    Expired,

    [PgName("PAUSED")]
    Paused,
}
