package rallly.models

enum class ScheduledEventInviteStatus(
    val dbName: String,
) {
    PENDING("pending"),
    ACCEPTED("accepted"),
    DECLINED("declined"),
    TENTATIVE("tentative"),
}
