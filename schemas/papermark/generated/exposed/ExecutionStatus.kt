package papermark.models

enum class ExecutionStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    IN_PROGRESS("IN_PROGRESS"),
    COMPLETED("COMPLETED"),
    FAILED("FAILED"),
    BLOCKED("BLOCKED"),
}
