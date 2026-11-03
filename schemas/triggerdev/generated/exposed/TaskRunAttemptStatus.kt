package triggerdev.models

enum class TaskRunAttemptStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    EXECUTING("EXECUTING"),
    PAUSED("PAUSED"),
    FAILED("FAILED"),
    CANCELED("CANCELED"),
    COMPLETED("COMPLETED"),
}
