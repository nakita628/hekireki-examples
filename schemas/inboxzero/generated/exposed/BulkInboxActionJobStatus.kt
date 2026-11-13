package inboxzero.models

enum class BulkInboxActionJobStatus(
    val dbName: String,
) {
    RUNNING("RUNNING"),
    COMPLETED("COMPLETED"),
    FAILED("FAILED"),
    CANCELLED("CANCELLED"),
}
