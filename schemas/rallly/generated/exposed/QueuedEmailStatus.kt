package rallly.models

enum class QueuedEmailStatus(
    val dbName: String,
) {
    PENDING("pending"),
    SENT("sent"),
    SKIPPED("skipped"),
    FAILED("failed"),
}
