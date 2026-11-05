package inboxzero.models

enum class DocumentFilingStatus(
    val dbName: String,
) {
    PROCESSING("PROCESSING"),
    PENDING("PENDING"),
    FILED("FILED"),
    REJECTED("REJECTED"),
    ERROR("ERROR"),
    PREVIEW("PREVIEW"),
}
