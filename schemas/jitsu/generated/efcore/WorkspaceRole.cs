#nullable enable

using NpgsqlTypes;

namespace Jitsu.Models;

public enum WorkspaceRole
{
    [PgName("owner")]
    Owner,

    [PgName("editor")]
    Editor,

    [PgName("analyst")]
    Analyst,
}
