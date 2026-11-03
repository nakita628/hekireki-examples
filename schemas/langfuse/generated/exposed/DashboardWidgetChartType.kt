package langfuse.models

enum class DashboardWidgetChartType(
    val dbName: String,
) {
    LINE_TIME_SERIES("LINE_TIME_SERIES"),
    AREA_TIME_SERIES("AREA_TIME_SERIES"),
    BAR_TIME_SERIES("BAR_TIME_SERIES"),
    HORIZONTAL_BAR("HORIZONTAL_BAR"),
    VERTICAL_BAR("VERTICAL_BAR"),
    PIE("PIE"),
    NUMBER("NUMBER"),
    HISTOGRAM("HISTOGRAM"),
    PIVOT_TABLE("PIVOT_TABLE"),
}
