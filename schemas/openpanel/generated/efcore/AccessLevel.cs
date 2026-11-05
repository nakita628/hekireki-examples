#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum AccessLevel
{
    [PgName("read")]
    Read,

    [PgName("write")]
    Write,

    [PgName("admin")]
    Admin,
}
