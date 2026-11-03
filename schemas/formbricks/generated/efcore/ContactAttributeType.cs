#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ContactAttributeType
{
    [PgName("default")]
    Default,

    [PgName("custom")]
    Custom,
}
