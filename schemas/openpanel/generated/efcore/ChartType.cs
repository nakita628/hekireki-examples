#nullable enable

using NpgsqlTypes;

namespace OpenPanel.Models;

public enum ChartType
{
    [PgName("linear")]
    Linear,

    [PgName("bar")]
    Bar,

    [PgName("histogram")]
    Histogram,

    [PgName("pie")]
    Pie,

    [PgName("metric")]
    Metric,

    [PgName("area")]
    Area,

    [PgName("map")]
    Map,

    [PgName("funnel")]
    Funnel,

    [PgName("retention")]
    Retention,

    [PgName("conversion")]
    Conversion,

    [PgName("sankey")]
    Sankey,
}
