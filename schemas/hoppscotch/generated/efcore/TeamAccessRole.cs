#nullable enable

using NpgsqlTypes;

namespace Hoppscotch.Models;

public enum TeamAccessRole
{
    [PgName("OWNER")]
    Owner,

    [PgName("VIEWER")]
    Viewer,

    [PgName("EDITOR")]
    Editor,
}
