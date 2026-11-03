#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum DocumentVisibility
{
    [PgName("EVERYONE")]
    Everyone,

    [PgName("MANAGER_AND_ABOVE")]
    ManagerAndAbove,

    [PgName("ADMIN")]
    Admin,
}
