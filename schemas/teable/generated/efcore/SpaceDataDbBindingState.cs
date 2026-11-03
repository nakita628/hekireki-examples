#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum SpaceDataDbBindingState
{
    [PgName("ready")]
    Ready,

    [PgName("validating")]
    Validating,

    [PgName("initializing")]
    Initializing,

    [PgName("migrating")]
    Migrating,

    [PgName("error")]
    Error,

    [PgName("disabled")]
    Disabled,
}
