package triggerdev.models

enum class TaskQueueType(
    val dbName: String,
) {
    VIRTUAL("VIRTUAL"),
    NAMED("NAMED"),
}
