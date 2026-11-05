#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum DocumentFilingStatus
{
    [PgName("PROCESSING")]
    Processing,

    [PgName("PENDING")]
    Pending,

    [PgName("FILED")]
    Filed,

    [PgName("REJECTED")]
    Rejected,

    [PgName("ERROR")]
    Error,

    [PgName("PREVIEW")]
    Preview,
}
