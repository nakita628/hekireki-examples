#nullable enable

using NpgsqlTypes;

namespace Linkwarden.Models;

public enum AppMigrationStatus
{
    [PgName("APPLIED")]
    Applied,

    [PgName("PENDING")]
    Pending,

    [PgName("FAILED")]
    Failed,
}
