#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MeetingBriefingStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("SENT")]
    Sent,

    [PgName("FAILED")]
    Failed,

    [PgName("SKIPPED")]
    Skipped,
}
