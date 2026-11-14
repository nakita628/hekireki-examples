#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum VirtualKeyStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("DISABLED")]
    Disabled,

    [PgName("REVOKED")]
    Revoked,
}
