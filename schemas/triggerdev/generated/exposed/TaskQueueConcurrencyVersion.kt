package triggerdev.models

enum class TaskQueueConcurrencyVersion(
    val dbName: String,
) {
    V1("V1"),
    V2("V2"),
}
