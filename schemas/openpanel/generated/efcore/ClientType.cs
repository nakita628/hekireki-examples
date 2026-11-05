#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum ClientType
{
    [PgName("read")]
    Read,

    [PgName("write")]
    Write,

    [PgName("root")]
    Root,
}
