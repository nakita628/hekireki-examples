package langfuse.models

enum class BlobStorageIntegrationFileType(
    val dbName: String,
) {
    JSON("JSON"),
    CSV("CSV"),
    JSONL("JSONL"),
    PARQUET("PARQUET"),
}
