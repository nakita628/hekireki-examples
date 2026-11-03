package langfuse.models

enum class BlobStorageExportMode(
    val dbName: String,
) {
    FULL_HISTORY("FULL_HISTORY"),
    FROM_TODAY("FROM_TODAY"),
    FROM_CUSTOM_DATE("FROM_CUSTOM_DATE"),
}
