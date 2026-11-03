package langfuse.models

enum class MonitorSeverity(
    val dbName: String,
) {
    PAUSED("PAUSED"),
    UNKNOWN("UNKNOWN"),
    NO_DATA("NO_DATA"),
    OK("OK"),
    WARNING("WARNING"),
    ALERT("ALERT"),
}
