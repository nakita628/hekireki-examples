package triggerdev.models

enum class BulkActionItemStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    COMPLETED("COMPLETED"),
    FAILED("FAILED"),
}
