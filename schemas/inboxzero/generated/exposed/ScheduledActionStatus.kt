package inboxzero.models

enum class ScheduledActionStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    EXECUTING("EXECUTING"),
    COMPLETED("COMPLETED"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
