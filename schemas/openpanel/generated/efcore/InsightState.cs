#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum InsightState
{
    [PgName("active")]
    Active,

    [PgName("suppressed")]
    Suppressed,

    [PgName("closed")]
    Closed,
}
