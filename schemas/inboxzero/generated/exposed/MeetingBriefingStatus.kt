package inboxzero.models

enum class MeetingBriefingStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    SENT("SENT"),
    FAILED("FAILED"),
    SKIPPED("SKIPPED"),
}
