#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum SpaceType
{
    [PgName("personal")]
    Personal,

    [PgName("work")]
    Work,
}
