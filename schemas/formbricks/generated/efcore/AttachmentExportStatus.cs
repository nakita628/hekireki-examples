#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum AttachmentExportStatus
{
    [PgName("queued")]
    Queued,

    [PgName("running")]
    Running,

    [PgName("completed")]
    Completed,

    [PgName("failed")]
    Failed,

    [PgName("expired")]
    Expired,
}
