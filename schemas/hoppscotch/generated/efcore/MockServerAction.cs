#nullable enable

using NpgsqlTypes;

namespace Hoppscotch.Models;

public enum MockServerAction
{
    [PgName("CREATED")]
    Created,

    [PgName("DELETED")]
    Deleted,

    [PgName("ACTIVATED")]
    Activated,

    [PgName("DEACTIVATED")]
    Deactivated,
}
