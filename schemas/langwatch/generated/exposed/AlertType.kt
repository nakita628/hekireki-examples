package langwatch.models

enum class AlertType(
    val dbName: String,
) {
    CRITICAL("CRITICAL"),
    WARNING("WARNING"),
    INFO("INFO"),
}
