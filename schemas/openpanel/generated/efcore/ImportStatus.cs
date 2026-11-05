#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum ImportStatus
{
    [PgName("pending")]
    Pending,

    [PgName("processing")]
    Processing,

    [PgName("completed")]
    Completed,

    [PgName("failed")]
    Failed,
}
