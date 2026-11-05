#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ReferralStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,
}
