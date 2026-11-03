package triggerdev.models

enum class WorkerDeploymentType(
    val dbName: String,
) {
    MANAGED("MANAGED"),
    UNMANAGED("UNMANAGED"),
    V1("V1"),
}
