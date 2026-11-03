#nullable enable

using NpgsqlTypes;

namespace Typebot.Models;

public enum WorkspaceRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,

    [PgName("GUEST")]
    Guest,
}
