package triggerdev.models

enum class BatchTaskRunStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    PROCESSING("PROCESSING"),
    COMPLETED("COMPLETED"),
    PARTIAL_FAILED("PARTIAL_FAILED"),
    ABORTED("ABORTED"),
}
