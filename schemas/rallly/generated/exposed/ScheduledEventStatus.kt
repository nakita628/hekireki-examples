package rallly.models

enum class ScheduledEventStatus(
    val dbName: String,
) {
    CONFIRMED("confirmed"),
    CANCELED("canceled"),
    UNCONFIRMED("unconfirmed"),
}
