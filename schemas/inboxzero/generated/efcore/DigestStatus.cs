#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum DigestStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("SENT")]
    Sent,

    [PgName("FAILED")]
    Failed,
}
