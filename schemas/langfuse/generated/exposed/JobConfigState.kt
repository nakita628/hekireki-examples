package langfuse.models

enum class JobConfigState(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    INACTIVE("INACTIVE"),
}
