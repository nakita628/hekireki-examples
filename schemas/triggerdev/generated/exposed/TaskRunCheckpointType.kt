package triggerdev.models

enum class TaskRunCheckpointType(
    val dbName: String,
) {
    DOCKER("DOCKER"),
    KUBERNETES("KUBERNETES"),
    COMPUTE("COMPUTE"),
}
