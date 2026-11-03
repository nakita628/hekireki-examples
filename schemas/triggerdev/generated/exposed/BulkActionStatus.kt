package triggerdev.models

enum class BulkActionStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    COMPLETED("COMPLETED"),
    ABORTED("ABORTED"),
}
