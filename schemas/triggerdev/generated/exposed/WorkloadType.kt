package triggerdev.models

enum class WorkloadType(
    val dbName: String,
) {
    CONTAINER("CONTAINER"),
    MICROVM("MICROVM"),
}
