package triggerdev.models

enum class CheckpointRestoreEventType(
    val dbName: String,
) {
    CHECKPOINT("CHECKPOINT"),
    RESTORE("RESTORE"),
}
