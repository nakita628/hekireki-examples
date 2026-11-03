package triggerdev.models

enum class TaskQueueVersion(
    val dbName: String,
) {
    V1("V1"),
    V2("V2"),
}
