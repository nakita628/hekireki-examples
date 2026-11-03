package langfuse.models

enum class ExternalMediaStorageIntegrationType(
    val dbName: String,
) {
    S3("S3"),
    S3_COMPATIBLE("S3_COMPATIBLE"),
}
