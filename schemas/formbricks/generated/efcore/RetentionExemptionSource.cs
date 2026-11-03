#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionExemptionSource
{
    [PgName("manual")]
    Manual,

    [PgName("restore")]
    Restore,
}
