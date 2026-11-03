package triggerdev.models

enum class TaskQueueRole(
    val dbName: String,
) {
    QUEUE("QUEUE"),
    LIMIT("LIMIT"),
}
