#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum SpaceDataDbMigrationJobState
{
    [PgName("pending")]
    Pending,

    [PgName("waiting_worker")]
    WaitingWorker,

    [PgName("preflight")]
    Preflight,

    [PgName("freezing_writes")]
    FreezingWrites,

    [PgName("copying")]
    Copying,

    [PgName("validating")]
    Validating,

    [PgName("switching")]
    Switching,

    [PgName("succeeded")]
    Succeeded,

    [PgName("failed")]
    Failed,

    [PgName("canceled")]
    Canceled,

    [PgName("rolled_back")]
    RolledBack,
}
