#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum SpaceTier
{
    [PgName("hobby")]
    Hobby,

    [PgName("pro")]
    Pro,
}
