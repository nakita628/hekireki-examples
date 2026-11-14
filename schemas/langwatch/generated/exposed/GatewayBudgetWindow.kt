package langwatch.models

enum class GatewayBudgetWindow(
    val dbName: String,
) {
    MINUTE("MINUTE"),
    HOUR("HOUR"),
    DAY("DAY"),
    WEEK("WEEK"),
    MONTH("MONTH"),
    TOTAL("TOTAL"),
    MANUAL("MANUAL"),
}
