#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ThreadTrackerType
{
    [PgName("AWAITING")]
    Awaiting,

    [PgName("NEEDS_REPLY")]
    NeedsReply,

    [PgName("NEEDS_ACTION")]
    NeedsAction,
}
