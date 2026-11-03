#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ProjectVersion
{
    [PgName("V2")]
    V2,

    [PgName("V3")]
    V3,
}
