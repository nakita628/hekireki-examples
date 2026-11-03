package langfuse.models

enum class ActionExecutionStatus(
    val dbName: String,
) {
    COMPLETED("COMPLETED"),
    ERROR("ERROR"),
    PENDING("PENDING"),
    CANCELLED("CANCELLED"),
}
