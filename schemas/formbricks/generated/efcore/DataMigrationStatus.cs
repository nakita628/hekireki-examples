#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum DataMigrationStatus
{
    [PgName("pending")]
    Pending,

    [PgName("applied")]
    Applied,

    [PgName("failed")]
    Failed,
}
