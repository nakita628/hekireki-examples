package inboxzero.models

enum class AutomationJobRunStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    RUNNING("RUNNING"),
    SENT("SENT"),
    SKIPPED("SKIPPED"),
    FAILED("FAILED"),
}
