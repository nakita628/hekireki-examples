package triggerdev.models

enum class ProjectAlertChannelType(
    val dbName: String,
) {
    EMAIL("EMAIL"),
    SLACK("SLACK"),
    WEBHOOK("WEBHOOK"),
}
