#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ResponseOutcome
{
    [PgName("lowQuality")]
    LowQuality,
}
