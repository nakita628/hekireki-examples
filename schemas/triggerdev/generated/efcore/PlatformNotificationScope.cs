#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum PlatformNotificationScope
{
    [PgName("USER")]
    User,

    [PgName("PROJECT")]
    Project,

    [PgName("ORGANIZATION")]
    Organization,

    [PgName("GLOBAL")]
    Global,
}
