#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum DashboardWidgetChartType
{
    [PgName("LINE_TIME_SERIES")]
    LineTimeSeries,

    [PgName("AREA_TIME_SERIES")]
    AreaTimeSeries,

    [PgName("BAR_TIME_SERIES")]
    BarTimeSeries,

    [PgName("HORIZONTAL_BAR")]
    HorizontalBar,

    [PgName("VERTICAL_BAR")]
    VerticalBar,

    [PgName("PIE")]
    Pie,

    [PgName("NUMBER")]
    Number,

    [PgName("HISTOGRAM")]
    Histogram,

    [PgName("PIVOT_TABLE")]
    PivotTable,
}
