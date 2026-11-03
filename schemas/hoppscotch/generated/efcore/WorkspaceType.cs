#nullable enable

using NpgsqlTypes;

namespace Hoppscotch.Models;

public enum WorkspaceType
{
    [PgName("USER")]
    User,

    [PgName("TEAM")]
    Team,
}
