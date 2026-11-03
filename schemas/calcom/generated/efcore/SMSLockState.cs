#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum SMSLockState
{
    [PgName("LOCKED")]
    Locked,

    [PgName("UNLOCKED")]
    Unlocked,

    [PgName("REVIEW_NEEDED")]
    ReviewNeeded,
}
