package inboxzero.models

enum class ScheduledEmailStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    PROCESSING("PROCESSING"),
    SENT("SENT"),
    BLOCKED_AUTH("BLOCKED_AUTH"),
    UNCERTAIN("UNCERTAIN"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
