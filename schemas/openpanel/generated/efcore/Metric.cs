#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum Metric
{
    [PgName("sum")]
    Sum,

    [PgName("average")]
    Average,

    [PgName("min")]
    Min,

    [PgName("max")]
    Max,

    [PgName("count")]
    Count,
}
