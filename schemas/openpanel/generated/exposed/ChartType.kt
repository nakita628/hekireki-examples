package openpanel.models

enum class ChartType(
    val dbName: String,
) {
    LINEAR("linear"),
    BAR("bar"),
    HISTOGRAM("histogram"),
    PIE("pie"),
    METRIC("metric"),
    AREA("area"),
    MAP("map"),
    FUNNEL("funnel"),
    RETENTION("retention"),
    CONVERSION("conversion"),
    SANKEY("sankey"),
}
