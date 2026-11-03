#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ApiKeyPermission
{
    [PgName("read")]
    Read,

    [PgName("write")]
    Write,

    [PgName("manage")]
    Manage,
}
