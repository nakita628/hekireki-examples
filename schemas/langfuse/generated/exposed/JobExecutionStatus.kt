package langfuse.models

enum class JobExecutionStatus(
    val dbName: String,
) {
    COMPLETED("COMPLETED"),
    ERROR("ERROR"),
    PENDING("PENDING"),
    CANCELLED("CANCELLED"),
    DELAYED("DELAYED"),
}
