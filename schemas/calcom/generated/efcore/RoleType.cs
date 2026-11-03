#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum RoleType
{
    [PgName("SYSTEM")]
    System,

    [PgName("CUSTOM")]
    Custom,
}
