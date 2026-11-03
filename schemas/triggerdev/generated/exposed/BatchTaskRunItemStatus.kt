package triggerdev.models

enum class BatchTaskRunItemStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    FAILED("FAILED"),
    CANCELED("CANCELED"),
    COMPLETED("COMPLETED"),
}
