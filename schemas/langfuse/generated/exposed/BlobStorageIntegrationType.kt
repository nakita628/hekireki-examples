package langfuse.models

enum class BlobStorageIntegrationType(
    val dbName: String,
) {
    S3("S3"),
    S3_COMPATIBLE("S3_COMPATIBLE"),
    AZURE_BLOB_STORAGE("AZURE_BLOB_STORAGE"),
    GOOGLE_CLOUD_STORAGE("GOOGLE_CLOUD_STORAGE"),
}
