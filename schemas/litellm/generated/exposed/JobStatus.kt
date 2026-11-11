package litellm.models

enum class JobStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    INACTIVE("INACTIVE"),
}
