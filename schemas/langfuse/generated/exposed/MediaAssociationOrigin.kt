package langfuse.models

enum class MediaAssociationOrigin(
    val dbName: String,
) {
    UNKNOWN("UNKNOWN"),
    CLIENT_UPLOAD("CLIENT_UPLOAD"),
    INGESTION_MEDIA_EXTRACTION("INGESTION_MEDIA_EXTRACTION"),
    INGESTION_FIELD_OVERFLOW("INGESTION_FIELD_OVERFLOW"),
}
