package openpanel.models

enum class InsightState(
    val dbName: String,
) {
    ACTIVE("active"),
    SUPPRESSED("suppressed"),
    CLOSED("closed"),
}
