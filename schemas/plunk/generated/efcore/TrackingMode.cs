#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum TrackingMode
{
    [PgName("ENABLED")]
    Enabled,

    [PgName("DISABLED")]
    Disabled,

    [PgName("MARKETING_ONLY")]
    MarketingOnly,
}
