#nullable enable

using NpgsqlTypes;

namespace Typebot.Models;

public enum Plan
{
    [PgName("FREE")]
    Free,

    [PgName("STARTER")]
    Starter,

    [PgName("PRO")]
    Pro,

    [PgName("LIFETIME")]
    Lifetime,

    [PgName("OFFERED")]
    Offered,

    [PgName("CUSTOM")]
    Custom,

    [PgName("UNLIMITED")]
    Unlimited,

    [PgName("ENTERPRISE")]
    Enterprise,
}
