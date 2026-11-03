#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum SpaceDataDbBindingMode
{
    [PgName("default")]
    Default,

    [PgName("byodb")]
    Byodb,
}
