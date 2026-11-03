package langfuse.models

enum class DatasetStatus(
    val dbName: String,
) {
    ACTIVE("ACTIVE"),
    ARCHIVED("ARCHIVED"),
}
