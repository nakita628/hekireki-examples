package triggerdev.models

enum class TaskTriggerSource(
    val dbName: String,
) {
    STANDARD("STANDARD"),
    SCHEDULED("SCHEDULED"),
    AGENT("AGENT"),
    WEBHOOK("WEBHOOK"),
}
