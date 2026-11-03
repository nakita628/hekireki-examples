package langfuse.models

enum class MonitorThresholdOperator(
    val dbName: String,
) {
    GT("GT"),
    GTE("GTE"),
    LT("LT"),
    LTE("LTE"),
    EQ("EQ"),
    NEQ("NEQ"),
}
