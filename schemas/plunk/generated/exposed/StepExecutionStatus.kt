package plunk.models

enum class StepExecutionStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    SCHEDULED("SCHEDULED"),
    WAITING("WAITING"),
    RUNNING("RUNNING"),
    COMPLETED("COMPLETED"),
    SKIPPED("SKIPPED"),
    FAILED("FAILED"),
}
