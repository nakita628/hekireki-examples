package inboxzero.models

enum class SchedulingStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    SCHEDULED("SCHEDULED"),
    FAILED("FAILED"),
}
