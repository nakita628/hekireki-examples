package formbricks.models

enum class WorkflowStatus(
    val dbName: String,
) {
    DRAFT("draft"),
    ENABLED("enabled"),
    DISABLED("disabled"),
    ARCHIVED("archived"),
}
