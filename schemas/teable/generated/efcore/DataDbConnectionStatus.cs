#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum DataDbConnectionStatus
{
    [PgName("pending")]
    Pending,

    [PgName("validating")]
    Validating,

    [PgName("ready")]
    Ready,

    [PgName("error")]
    Error,

    [PgName("migrating")]
    Migrating,

    [PgName("disabled")]
    Disabled,
}
