package triggerdev.models

enum class BulkActionNotificationType(
    val dbName: String,
) {
    NONE("NONE"),
    EMAIL("EMAIL"),
}
