package formbricks.models

enum class WorkflowRunLogStatus(
    val dbName: String,
) {
    PENDING("pending"),
    RUNNING("running"),
    SUCCEEDED("succeeded"),
    FAILED("failed"),
    SKIPPED("skipped"),
}
