package triggerdev.models

enum class WorkerInstanceGroupType(
    val dbName: String,
) {
    MANAGED("MANAGED"),
    UNMANAGED("UNMANAGED"),
}
