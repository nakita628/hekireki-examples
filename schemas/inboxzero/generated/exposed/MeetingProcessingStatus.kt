package inboxzero.models

enum class MeetingProcessingStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    PROCESSING("PROCESSING"),
    COMPLETED("COMPLETED"),
    FAILED("FAILED"),
}
