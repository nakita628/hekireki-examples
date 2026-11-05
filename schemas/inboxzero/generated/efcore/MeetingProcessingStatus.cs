#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MeetingProcessingStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("COMPLETED")]
    Completed,

    [PgName("FAILED")]
    Failed,
}
