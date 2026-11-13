package litellm.models

enum class ScheduledJobRunStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    RUNNING("RUNNING"),
    SUCCEEDED("SUCCEEDED"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
