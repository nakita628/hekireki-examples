package triggerdev.models

enum class BulkActionType(
    val dbName: String,
) {
    CANCEL("CANCEL"),
    REPLAY("REPLAY"),
}
