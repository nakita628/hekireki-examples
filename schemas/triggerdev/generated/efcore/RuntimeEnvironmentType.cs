#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum RuntimeEnvironmentType
{
    [PgName("PRODUCTION")]
    Production,

    [PgName("STAGING")]
    Staging,

    [PgName("DEVELOPMENT")]
    Development,

    [PgName("PREVIEW")]
    Preview,
}
