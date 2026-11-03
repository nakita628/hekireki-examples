package papermark.models

enum class DocumentStorageType(
    val dbName: String,
) {
    S3_PATH("S3_PATH"),
    VERCEL_BLOB("VERCEL_BLOB"),
}
