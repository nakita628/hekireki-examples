#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum PlatformNotificationSurface
{
    [PgName("WEBAPP")]
    Webapp,

    [PgName("CLI")]
    Cli,
}
