#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum SsoConnectionMigrationPhase
{
    [PgName("SETUP")]
    Setup,

    [PgName("GRACE_LEGACY")]
    GraceLegacy,

    [PgName("GRACE_DIRECT")]
    GraceDirect,

    [PgName("FINALIZING")]
    Finalizing,

    [PgName("FINALIZED")]
    Finalized,
}
