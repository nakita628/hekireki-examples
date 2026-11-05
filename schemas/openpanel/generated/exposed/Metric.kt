package openpanel.models

enum class Metric(
    val dbName: String,
) {
    SUM("sum"),
    AVERAGE("average"),
    MIN("min"),
    MAX("max"),
    COUNT("count"),
}
