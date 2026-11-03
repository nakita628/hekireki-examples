#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum WorkspaceTeamPermission
{
    [PgName("read")]
    Read,

    [PgName("readWrite")]
    ReadWrite,

    [PgName("manage")]
    Manage,
}
