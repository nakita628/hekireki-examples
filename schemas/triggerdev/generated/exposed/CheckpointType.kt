package triggerdev.models

enum class CheckpointType(
    val dbName: String,
) {
    DOCKER("DOCKER"),
    KUBERNETES("KUBERNETES"),
}
