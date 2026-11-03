package langfuse.models

enum class MonitorStatus(
    val dbName: String,
) {
    PAUSED("PAUSED"),
    ACTIVE("ACTIVE"),
    ERROR_BAD_QUERY("ERROR_BAD_QUERY"),
}
