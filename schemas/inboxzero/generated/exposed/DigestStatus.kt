package inboxzero.models

enum class DigestStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    PROCESSING("PROCESSING"),
    SENT("SENT"),
    FAILED("FAILED"),
}
