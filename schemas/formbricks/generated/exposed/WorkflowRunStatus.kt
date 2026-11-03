package formbricks.models

enum class WorkflowRunStatus(
    val dbName: String,
) {
    QUEUED("queued"),
    RUNNING("running"),
    COMPLETED("completed"),
    FAILED("failed"),
    CANCELED("canceled"),
}
