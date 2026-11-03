package calcom.models

enum class CalendarCacheEventStatus(
    val dbName: String,
) {
    CONFIRMED("confirmed"),
    TENTATIVE("tentative"),
    CANCELLED("cancelled"),
}
