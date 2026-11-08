package plunk.models

enum class WorkflowExecutionStatus(
    val dbName: String,
) {
    RUNNING("RUNNING"),
    WAITING("WAITING"),
    COMPLETED("COMPLETED"),
    EXITED("EXITED"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
